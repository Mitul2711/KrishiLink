using KrishiLink.DBContext;
using KrishiLink.Models.Auth;
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
                new SqlParameter("@Access_Token", ""),
                new SqlParameter("@Business_Name", userData.Business_Name ?? (object)DBNull.Value),
                new SqlParameter("@Email", userData.Email ?? (object)DBNull.Value),
                new SqlParameter("@Password", userData.Password ?? (object)DBNull.Value),
                new SqlParameter("@ZipCode", userData.ZipCode ?? (object)DBNull.Value),
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                "EXEC UserRegister @Action, @UserId, @Mobile, @Access_Token, @Business_Name, @Email, @Password, @ZipCode",
                parameters
            );
        }


        public async Task UpdateUserAsync(int userId, string accessToken)
        {
            var parameters = new[]
            {
                new SqlParameter("@Action", "PUT"),
                new SqlParameter("@UserId",  userId),
                new SqlParameter("@Mobile", DBNull.Value),
                new SqlParameter("@Access_Token", accessToken ?? (object)DBNull.Value),
                new SqlParameter("@Business_Name", DBNull.Value),
                new SqlParameter("@Email",  DBNull.Value),
                new SqlParameter("@Password",  DBNull.Value),
                new SqlParameter("@ZipCode",  DBNull.Value)
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                "EXEC dbo.UserRegister " +
                "@Action, @UserId, @Mobile, @Access_Token, " +
                "@Business_Name, @Email, @Password, @ZipCode",
                parameters
            );
        }

        public async Task<UserData> CheckAccess(int userId, string accessToken)
        {
            var user = await _appDbContext.UserData
                    .FromSqlInterpolated($"EXEC UserRegister @Action = {"CHECK"}, @UserId = {userId}, @Access_Token = {accessToken}")
                    .ToListAsync();

            return user.FirstOrDefault();

        }

    }
}
