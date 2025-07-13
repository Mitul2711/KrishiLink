using KrishiLink.DTO;
using KrishiLink.Models.Auth;

namespace KrishiLink.Repository.Auth.Interface
{
    public interface IUserRepository
    {
        public Task<UserData> GetUserAsync(string mobile);
        public Task RegisterUser(UserData userData);

        public Task UpdateUserAsync(int userId, string accessToken);

        public Task<UserData> CheckAccess(int userId, string accessToken);

    }
}
