using KrishiLink.DBContext;
using KrishiLink.DTO.Broker;
using KrishiLink.Models.Broker;
using KrishiLink.Repository.Broker.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KrishiLink.Repository.Broker
{
    public class BrokerDataRepository : IBrokerDataRepository
    {
        private readonly AppDbContext _appDbContext;

        public BrokerDataRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<IEnumerable<BrokerData>> GetAllBrokerData(int userId, string access_token)
        {
            var actionParam = new SqlParameter("@Action", "GET");

            var brokerData = await _appDbContext.BrokerData.FromSqlInterpolated($"EXEC ManageBrokerData @Action = {"GET"}, @UserId = {userId}").ToListAsync();
            return brokerData;
        }

        public async Task<BrokerData> GetBrokerDataById(int id, int userId, string access_token)
        {
            var actionParam = new SqlParameter("@Action", "GET");

            var brokerData = await _appDbContext.BrokerData.FromSqlInterpolated(
                $"EXEC ManageBrokerData @Action = {"GET"}, @BrokerID = {id}, @UserId = {userId}"
                ).ToListAsync();

            return brokerData.FirstOrDefault();
        }

        public async Task AddBrokerData(BrokerDataDTO brokerDataDTO)
        {
            var parameters = new[]
            {
                new SqlParameter("@Action", "POST"),
                new SqlParameter("@BrokerId", DBNull.Value),
                new SqlParameter("@Broker_name", brokerDataDTO.Broker_name ?? (object)DBNull.Value),
                new SqlParameter("@Mobile", brokerDataDTO.Mobile ?? (object)DBNull.Value),
                new SqlParameter("@Village", brokerDataDTO.Village ?? (object)DBNull.Value),
                new SqlParameter("@Crop_Name", brokerDataDTO.Crop_Name ?? (object)DBNull.Value),
                new SqlParameter("@Crop_Type", brokerDataDTO.Crop_Type ?? (object)DBNull.Value),
                new SqlParameter("@Weight", brokerDataDTO.Weight),
                new SqlParameter("@Price", brokerDataDTO.Price),
                new SqlParameter("@Count", brokerDataDTO.Count),
                new SqlParameter("@Total_Brokerage", brokerDataDTO.Total_Brokerage),
                new SqlParameter("@Total_Amount", brokerDataDTO.Total_Amount),
                new SqlParameter("@UserId", brokerDataDTO.UserId),
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                "EXEC ManageBrokerData @Action, @BrokerId, @Broker_name, @Mobile, @Village, @Crop_Name, @Crop_Type, @Weight, @Price, @Count, " +
                "@Total_Brokerage, @Total_Amount, @UserId",
                parameters
            );
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateBrokerData(BrokerSaleTokenDTO brokerSaleTokenDTO)
        {
            var parameters = new[]
            {
                new SqlParameter("@Action", "PUT"),
                new SqlParameter("@BrokerId", brokerSaleTokenDTO.BrokerId),
                new SqlParameter("@Broker_name", brokerSaleTokenDTO.Broker_name ?? (object)DBNull.Value),
                new SqlParameter("@Mobile", brokerSaleTokenDTO.Mobile ?? (object)DBNull.Value),
                new SqlParameter("@Village", brokerSaleTokenDTO.Village ?? (object)DBNull.Value),
                new SqlParameter("@Crop_Name", brokerSaleTokenDTO.Crop_Name ?? (object)DBNull.Value),
                new SqlParameter("@Crop_Type", brokerSaleTokenDTO.Crop_Type ?? (object)DBNull.Value),
                new SqlParameter("@Weight", brokerSaleTokenDTO.Weight),
                new SqlParameter("@Price", brokerSaleTokenDTO.Price),
                new SqlParameter("@Count", brokerSaleTokenDTO.Count),
                new SqlParameter("@Total_Brokerage", brokerSaleTokenDTO.Total_Brokerage),
                new SqlParameter("@Total_Amount", brokerSaleTokenDTO.Total_Amount),
                new SqlParameter("@UserId", brokerSaleTokenDTO.UserId),
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                "EXEC ManageBrokerData @Action, @BrokerId, @Broker_name, @Mobile, @Village, @Crop_Name, @Crop_Type, @Weight, @Price, @Count, " +
                "@Total_Brokerage, @Total_Amount, @UserId",
                parameters
            );
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteBrokerData(int id, int userId)
        {

            var parameters = new[]
           {
                new SqlParameter("@Action", "DELETE"),
                new SqlParameter("@BrokerId", id),
                new SqlParameter("@UserId", userId)
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                  "EXEC ManageBrokerData @Action = @Action, @BrokerId = @BrokerId, @UserId = @UserId",
                  parameters
            );

            await _appDbContext.SaveChangesAsync();

        }


    }
}
