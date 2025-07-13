using KrishiLink.Models.Auth;

namespace KrishiLink.Service.Auth.Interface
{
    public interface IUserService
    {
        public Task<(string status_code, string status_message)> RegisterUser(Register register);
        public Task<(string status_code, string status_message, string Token)> LoginUser(Login login);

    }
}
