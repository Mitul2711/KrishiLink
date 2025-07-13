using KrishiLink.Models.Auth;

namespace KrishiLink.Service.Auth.Interface
{
    public interface IUserService
    {
        public Task<(string status_code, string Message)> RegisterUser(Register register);
        public Task<(string status_code, string Message, string Token)> LoginUser(Login login);

    }
}
