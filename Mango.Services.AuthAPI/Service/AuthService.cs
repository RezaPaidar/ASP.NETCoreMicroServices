using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.applicationUsers
                .FirstOrDefault(x => x.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if ((user == null || !isValid))
            {
                return new LoginResponseDTO { User = null, Token = string.Empty };
            }

            UserDTO userDto = new()
            {
                ID = user.Id,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
            };

            LoginResponseDTO loginResponseDTO = new LoginResponseDTO
            {
                User = userDto,
                Token = string.Empty,
            };

            return loginResponseDTO;
        }

        public async Task<string> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDTO.Email,
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                Name = registrationRequestDTO.Name,
                PhoneNumber = registrationRequestDTO.PhoneNumber,
            };
            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.applicationUsers.First(x => x.UserName == registrationRequestDTO.Email);
                    UserDTO userDto = new()
                    {
                        Email = userToReturn.Email,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber,
                        ID = userToReturn.Id,

                    };

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }
            return "Error encountered";
        }
    }
}
