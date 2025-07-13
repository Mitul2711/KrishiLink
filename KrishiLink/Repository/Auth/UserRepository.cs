using KrishiLink.DBContext;
using KrishiLink.Models.Auth;
using KrishiLink.Models.Farmer;
using KrishiLink.Repository.Auth.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KrishiLink.Repository.Auth
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<UserData> GetUserAsync(string mobile)
        {
            var user = await _appDbContext.UserData
                    .FromSqlInterpolated($"EXEC UserRegister @Action = {"GET"}, @UserId = {DBNull.Value}, @Mobile = {mobile}")
                    .ToListAsync();

            return user.FirstOrDefault();
        }

        public async Task RegisterUser(UserData userData)
        {
            var parameters = new[]
           {
                new SqlParameter("@Action", "POST"),
                new SqlParameter("@UserId", DBNull.Value),
                new SqlParameter("@Mobile", userData.Mobile ?? (object)DBNull.Value),
                new SqlParameter("@Email", userData.Email ?? (object)DBNull.Value),
                new SqlParameter("@Business_Name", userData.Business_Name?? (object)DBNull.Value),
                new SqlParameter("@Password", userData.Password ?? (object)DBNull.Value),
                new SqlParameter("@ZipCode", userData.ZipCode?? (object)DBNull.Value),
            };

            var userExists = await _appDbContext.Database.ExecuteSqlRawAsync(
                    "EXEC UserRegister @Action, @UserId, @Mobile, @Email, @Business_Name, @Password, @ZipCode",
                    parameters
            );
            await _appDbContext.SaveChangesAsync();
        }
    }
}
