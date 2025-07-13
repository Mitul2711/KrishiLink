using KrishiLink.DBContext;
using KrishiLink.Models.Farmer;
using KrishiLink.Repository.Farmer.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KrishiLink.Repository.Farmer
{
    public class FarmerSaleRepository : IFarmerSaleRepository
    {
        private readonly AppDbContext _appDbContext;

        public FarmerSaleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<FarmerSale>> GetAllFarmerSaleDetails()
        {
            var actionParam = new SqlParameter("@Action", "GET");

            var farmerDetails = await _appDbContext.FarmerSales.FromSqlRaw("EXEC ManageFarmerSales @Action", actionParam).ToListAsync();
            return farmerDetails;
        }

        public async Task<FarmerSale> GetFarmerSaleDetailsById(int id)
        {
            var actionParam = new SqlParameter("@Action", "GET");

            var farmerDetail = await _appDbContext.FarmerSales.FromSqlInterpolated(
                $"EXEC ManageFarmerSales @Action = {"GET"}, @FarmerID = {id}"
                ).ToListAsync();

            return farmerDetail.FirstOrDefault();
        }

        public async Task AddFarmerSaleDetails(FarmerSale farmerSale)
        {
            var parameters = new[]
            {
                new SqlParameter("@Action", "POST"),
                new SqlParameter("@FarmerId", DBNull.Value),
                new SqlParameter("@Farmer_name", farmerSale.Farmer_name ?? (object)DBNull.Value),
                new SqlParameter("@Mobile", farmerSale.Mobile ?? (object)DBNull.Value),
                new SqlParameter("@Village", farmerSale.Village ?? (object)DBNull.Value),
                new SqlParameter("@Crop_Name", farmerSale.Crop_Name ?? (object)DBNull.Value),
                new SqlParameter("@Crop_Type", farmerSale.Crop_Type ?? (object)DBNull.Value),
                new SqlParameter("@Weight", farmerSale.Weight),
                 new SqlParameter("@Price", farmerSale.Price)
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                "EXEC ManageFarmerSales @Action, @FarmerId, @Farmer_name, @Mobile, @Village, @Crop_Name, @Crop_Type, @Weight, @Price",
                parameters
            );
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateFarmerSaleDetails(FarmerSale farmerSale)
        {
            var parameters = new[]
            {
                new SqlParameter("@Action", "PUT"),
                new SqlParameter("@FarmerId", farmerSale.FarmerId),
                new SqlParameter("@Farmer_name", farmerSale.Farmer_name ?? (object)DBNull.Value),
                new SqlParameter("@Mobile", farmerSale.Mobile ?? (object)DBNull.Value),
                new SqlParameter("@Village", farmerSale.Village ?? (object)DBNull.Value),
                new SqlParameter("@Crop_Name", farmerSale.Crop_Name ?? (object)DBNull.Value),
                new SqlParameter("@Crop_Type", farmerSale.Crop_Type ?? (object)DBNull.Value),
                new SqlParameter("@Weight", farmerSale.Weight),
                 new SqlParameter("@Price", farmerSale.Price)
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                "EXEC ManageFarmerSales @Action, @FarmerId, @Farmer_name, @Mobile, @Village, @Crop_Name, @Crop_Type, @Weight, @Price",
                parameters
            );
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteFarmerSaleDetails(int id)
        {
            var actionParam = new SqlParameter("@Action", "DELETE");

            await _appDbContext.Database.ExecuteSqlRawAsync(
                  "EXEC ManageFarmerSales @Action, @FarmerId",
                  actionParam,
                  new SqlParameter("@FarmerId", id)
            );

            await _appDbContext.SaveChangesAsync();

        }

    }
}
