using Dapper;
using KrishiLink.DBContext;
using KrishiLink.DTO.Dashboard;
using KrishiLink.DTO.Transport;
using KrishiLink.Models.Broker;
using KrishiLink.Models.DashBoard;
using KrishiLink.Models.Farmer;
using KrishiLink.Models.Transport;
using KrishiLink.Repository.Dashboard.Interface;
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
                    string category = dashboardDTO.Category?.ToUpper();

                    if (category == "FARMER")
                    {
                        result.Farmer = multi.Read<FarmerSale>().ToList();
                    }
                    else if (category == "TRANSPORT")
                    {
                        var transportList = multi.Read<DashVehicalDTO>().ToList();
                        var transferDetailList = multi.Read<DashTransportDTO>().ToList();

                        foreach (var transport in transportList)
                        {
                            transport.Transfer_Detail = transferDetailList
                                .Where(td => td.VehicalId == transport.VehicalId)
                                .ToList();
                        }

                        result.Transport = transportList;
                    }
                    else if (category == "BROKER")
                    {
                        result.Broker = multi.Read<BrokerData>().ToList();
                    }
                    else
                    {
                        result.Farmer = multi.Read<FarmerSale>().ToList();

                        var transportList = multi.Read<DashVehicalDTO>().ToList();
                        var transferDetailList = multi.Read<DashTransportDTO>().ToList();

                        foreach (var transport in transportList)
                        {
                            transport.Transfer_Detail = transferDetailList
                                .Where(td => td.VehicalId == transport.VehicalId)
                                .ToList();
                        }

                        result.Transport = transportList;

                        result.Broker = multi.Read<BrokerData>().ToList();
                    }
                }

                return result;
            }
        }

    }
}







