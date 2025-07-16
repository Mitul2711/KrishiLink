using KrishiLink.DBContext;
using KrishiLink.DTO.Farmer;
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

        public async Task<IEnumerable<FarmerSale>> GetAllFarmerSaleDetails(int userId)
        {

            var actionParam = new SqlParameter("@Action", "GET");

            var farmerDetail = await _appDbContext.FarmerSales.FromSqlInterpolated(
                $"EXEC ManageFarmerSales @Action = {"GET"}, @UserId = {userId}"
                ).ToListAsync();

            return farmerDetail;
        }

        public async Task<FarmerSale> GetFarmerSaleDetailsById(int id, int userId)
        {
            var actionParam = new SqlParameter("@Action", "GET");

            var farmerDetail = await _appDbContext.FarmerSales.FromSqlInterpolated(
                $"EXEC ManageFarmerSales @Action = {"GET"}, @FarmerID = {id}, @UserId = {userId}"
                ).ToListAsync();

            return farmerDetail.FirstOrDefault();
        }

        public async Task AddFarmerSaleDetails(FarmerSaleDTO farmerSale)
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
                new SqlParameter("@Price", farmerSale.Price),
                new SqlParameter("@Total_Price", farmerSale.Total_Price),
                new SqlParameter("@UserId", farmerSale.UserId)
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                "EXEC ManageFarmerSales " +
                "@Action, @FarmerId, @Farmer_name, @Mobile, @Village, " +
                "@Crop_Name, @Crop_Type, @Weight, @Price, @Total_Price, @UserId",
            parameters);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateFarmerSaleDetails(FarmerSaleTokenDTO farmerSaleTokenDTO)
        {
            var parameters = new[]
            {
                new SqlParameter("@Action", "PUT"),
                new SqlParameter("@FarmerId", farmerSaleTokenDTO.FarmerId),
                new SqlParameter("@Farmer_name", farmerSaleTokenDTO.Farmer_name ?? (object)DBNull.Value),
                new SqlParameter("@Mobile", farmerSaleTokenDTO.Mobile ?? (object)DBNull.Value),
                new SqlParameter("@Village", farmerSaleTokenDTO.Village ?? (object)DBNull.Value),
                new SqlParameter("@Crop_Name", farmerSaleTokenDTO.Crop_Name ?? (object)DBNull.Value),
                new SqlParameter("@Crop_Type", farmerSaleTokenDTO.Crop_Type ?? (object)DBNull.Value),
                new SqlParameter("@Weight", farmerSaleTokenDTO.Weight),
                new SqlParameter("@Price", farmerSaleTokenDTO.Price),
                new SqlParameter("@Total_Price", farmerSaleTokenDTO.Total_Price),
                new SqlParameter("@UserId", farmerSaleTokenDTO.FarmerId),
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                "EXEC ManageFarmerSales @Action, @FarmerId, @Farmer_name, @Mobile, @Village, @Crop_Name, @Crop_Type, @Weight, @Price," +
                " @Total_Price, @UserId",
                parameters
            );
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteFarmerSaleDetails(int id, int userId)
        {
            var parameters = new[]
            {
        new SqlParameter("@Action", "DELETE"),
        new SqlParameter("@FarmerId", id),
        new SqlParameter("@UserId", userId)
    };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                @"EXEC ManageFarmerSales 
              @Action = @Action, 
              @FarmerId = @FarmerId, 
              @UserId = @UserId",
                parameters);

            await _appDbContext.SaveChangesAsync();
        }


    }
}
