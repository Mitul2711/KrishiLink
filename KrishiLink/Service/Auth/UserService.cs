using KrishiLink.Models.Auth;
using KrishiLink.Repository.Auth.Interface;
using KrishiLink.Service.Auth.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KrishiLink.Service.Auth
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<Register> _passwordHasher;

        public IConfiguration _Configuration { get; }

        public UserService(IUserRepository userRepository, IPasswordHasher<Register> passwordHasher, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _Configuration = configuration;
        }

        public async Task<(string status_code, string Message)> RegisterUser(Register register)
        {
            var userExists = await _userRepository.GetUserAsync(register.Mobile);

            if (userExists != null)
            {
                return ("0", "User Already Exists!");
            }

            var hashPassword = _passwordHasher.HashPassword(register, register.Password);

            var userInfo = new UserData()
            {
                Mobile = register.Mobile,
                Email = register.Email,
                Business_Name = register.Business_Name,
                ZipCode = register.ZipCode,
                Password = hashPassword
            };
            await _userRepository.RegisterUser(userInfo);
            return ("1", "User Register Successfully!");
        }

        public async Task<(string status_code, string Message, string Token)> LoginUser(Login login)
        {
            var userExists = await _userRepository.GetUserAsync(login.Mobile);
            if (userExists == null)
            {
                return ("0", "User Not Exists", "");
            }
            var userInfo = new Register()
            {
                Mobile = userExists.Mobile,
                Email = userExists.Email,
                Password = userExists.Password,
                ZipCode = userExists.ZipCode,
                Business_Name = userExists.Business_Name,
            };
            var matchPassword = _passwordHasher.VerifyHashedPassword(userInfo, userExists.Password, login.Password);
            if(matchPassword == PasswordVerificationResult.Success)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_Configuration["Jwt:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("business_Name", userExists.Business_Name),
                        new Claim("zipCode", userExists.ZipCode),
                        new Claim("mobile", userExists.Mobile),
                        new Claim("email", userExists.Email),
                        new Claim("UserId", userExists.UserId.ToString(),  ClaimValueTypes.Integer),
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    Issuer = _Configuration["Jwt:Issuer"],
                    Audience = _Configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return ("1", "Login Successfully!", tokenHandler.WriteToken(token));
            } 
            else
            {
                return ("0", "Wrong Password!", "");
            }
        }

    }
}
