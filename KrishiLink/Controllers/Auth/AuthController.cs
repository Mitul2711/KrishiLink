using KrishiLink.Models.Auth;
using KrishiLink.Service.Auth.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KrishiLink.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> UserRegister(Register register)
        {
            var (status_code, status_message) = await _userService.RegisterUser(register);
            if (status_code == "0")
            {
                return NotFound(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message });
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(Login login)
        {
            var (status_code, status_message, data) = await _userService.LoginUser(login);
            if (status_code == "0")
            {
                return NotFound(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message, data });
            }
        }
    }
}
