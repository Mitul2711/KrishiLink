using Dapper;
using KrishiLink.DBContext;
using KrishiLink.DTO.Dashboard;
using KrishiLink.Models.Broker;
using KrishiLink.Models.DashBoard;
using KrishiLink.Models.Farmer;
using KrishiLink.Models.Transport;
using KrishiLink.Repository.Dashboard.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KrishiLink.Repository.Dashboard
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _appDbContext;
        public DashboardRepository(AppDbContext appDbContext)
        {   
            _appDbContext = appDbContext;
        }

        public async Task<DashBoard> GetDashBoardInfo(DashBoardDTO dashboardDTO)
        {
            using (var connection = _appDbContext.Database.GetDbConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync();

                var result = new DashBoard();

                using (var multi = await connection.QueryMultipleAsync("SP_DashBoard",
                    new { UserId = dashboardDTO.UserId, Category = dashboardDTO.Category, Period = dashboardDTO.Period },
                    commandType: CommandType.StoredProcedure))
                {
                    switch (dashboardDTO.Category?.ToUpper())
                    {
                        case "FARMER":
                            result.Farmer = multi.Read<FarmerSale>().ToList();
                            break;

                        case "TRANSPORT":
                            result.Transport = multi.Read<VehicleTransportData>().ToList();
                            break;

                        case "BROKER":
                            result.Broker = multi.Read<BrokerData>().ToList();
                            break;

                        default:
                            result.Farmer = multi.Read<FarmerSale>().ToList();
                            result.Transport = multi.Read<VehicleTransportData>().ToList();
                            result.Broker = multi.Read<BrokerData>().ToList();
                            break;
                    }
                }

                return result;
            }
        }

    }
}
