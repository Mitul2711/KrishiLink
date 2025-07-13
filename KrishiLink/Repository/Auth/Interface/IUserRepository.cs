using KrishiLink.Models.Auth;

namespace KrishiLink.Repository.Auth.Interface
{
    public interface IUserRepository
    {
        public Task<UserData> GetUserAsync(string mobile);
        public Task RegisterUser(UserData userData);

    }
}
