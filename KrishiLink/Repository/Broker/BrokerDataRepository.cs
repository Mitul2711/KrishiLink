using KrishiLink.DBContext;
using KrishiLink.Models.Broker;
using KrishiLink.Models.Farmer;
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


        public async Task<IEnumerable<BrokerData>> GetAllBrokerData()
        {
            var actionParam = new SqlParameter("@Action", "GET");

            var brokerData = await _appDbContext.BrokerData.FromSqlRaw("EXEC ManageBrokerData @Action", actionParam).ToListAsync();
            return brokerData;
        }

        public async Task<BrokerData> GetBrokerDataById(int id)
        {
            var actionParam = new SqlParameter("@Action", "GET");

            var brokerData = await _appDbContext.BrokerData.FromSqlInterpolated(
                $"EXEC ManageBrokerData @Action = {"GET"}, @BrokerID = {id}"
                ).ToListAsync();

            return brokerData.FirstOrDefault();
        }

        public async Task AddBrokerData(BrokerData brokerData)
        {
            var parameters = new[]
            {
                new SqlParameter("@Action", "POST"),
                new SqlParameter("@BrokerId", DBNull.Value),
                new SqlParameter("@Broker_name", brokerData.Broker_name ?? (object)DBNull.Value),
                new SqlParameter("@Mobile", brokerData.Mobile ?? (object)DBNull.Value),
                new SqlParameter("@Village", brokerData.Village ?? (object)DBNull.Value),
                new SqlParameter("@Crop_Name", brokerData.Crop_Name ?? (object)DBNull.Value),
                new SqlParameter("@Crop_Type", brokerData.Crop_Type ?? (object)DBNull.Value),
                new SqlParameter("@Weight", brokerData.Weight),
                new SqlParameter("@Price", brokerData.Price)
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                "EXEC ManageBrokerData @Action, @BrokerId, @Broker_name, @Mobile, @Village, @Crop_Name, @Crop_Type, @Weight, @Price",
                parameters
            );
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateBrokerData(BrokerData brokerData)
        {
            var parameters = new[]
            {
                new SqlParameter("@Action", "PUT"),
                new SqlParameter("@BrokerId", brokerData.BrokerId),
                new SqlParameter("@Broker_name", brokerData.Broker_name ?? (object)DBNull.Value),
                new SqlParameter("@Mobile", brokerData.Mobile ?? (object)DBNull.Value),
                new SqlParameter("@Village", brokerData.Village ?? (object)DBNull.Value),
                new SqlParameter("@Crop_Name", brokerData.Crop_Name ?? (object)DBNull.Value),
                new SqlParameter("@Crop_Type", brokerData.Crop_Type ?? (object)DBNull.Value),
                new SqlParameter("@Weight", brokerData.Weight),
                new SqlParameter("@Price", brokerData.Price)
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                "EXEC ManageBrokerData @Action, @BrokerId, @Broker_name, @Mobile, @Village, @Crop_Name, @Crop_Type, @Weight, @Price",
                parameters
            );
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteBrokerData(int id)
        {
            var actionParam = new SqlParameter("@Action", "DELETE");

            await _appDbContext.Database.ExecuteSqlRawAsync(
                  "EXEC ManageBrokerData @Action, @BrokerId",
                  actionParam,
                  new SqlParameter("@BrokerId", id)
            );

            await _appDbContext.SaveChangesAsync();

        }


    }
}
