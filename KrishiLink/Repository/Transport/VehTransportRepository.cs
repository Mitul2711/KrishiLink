using KrishiLink.DBContext;
using KrishiLink.DTO.Transport;
using KrishiLink.Models.Transport;
using KrishiLink.Repository.Transport.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;

namespace KrishiLink.Repository.Transport
{
    public class VehTransportRepository : IVehTransportRepository
    {
        private readonly AppDbContext _appDbContext;

        public VehTransportRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<CheckVehTransportDTO> CheckVehTransportData(int id)
        {
            var data = await _appDbContext.CheckVehTransportDTO.FromSqlInterpolated(
                    $"EXEC SP_InsertVehicleWithTransfers @Action = {"CHECK"}, @VehicalId = {id}"
                ).ToListAsync();

            return data.FirstOrDefault();
        }
        public async Task<IEnumerable<GetVehTransportDTO>> GetAllVehTransportDetail(int userId)
        {
            using var conn = _appDbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
        EXEC SP_InsertVehicleWithTransfers 
             @Action = @action, 
             @UserId = @uid";
            cmd.Parameters.Add(new SqlParameter("@action", "GET"));
            cmd.Parameters.Add(new SqlParameter("@uid", userId));

            using var rdr = await cmd.ExecuteReaderAsync();

            string json = null;
            if (await rdr.ReadAsync() && !rdr.IsDBNull(0))
            {
                json = rdr.GetString(0);
            }

            await conn.CloseAsync();

            return string.IsNullOrEmpty(json)
                   ? new List<GetVehTransportDTO>()
                   : JsonConvert.DeserializeObject<List<GetVehTransportDTO>>(json);
        }


        public async Task<IEnumerable<GetVehTransportDTO>> GetVehTransportDetailById(int id, int userId)
        {
            using var conn = _appDbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var command = conn.CreateCommand();
            command.CommandText = "EXEC SP_InsertVehicleWithTransfers @Action = @ActionParam, @VehicalId = @VehicalIdParam, @UserId=@UserId";

            var actionParam = command.CreateParameter();
            actionParam.ParameterName = "@ActionParam";
            actionParam.Value = "GET";
            command.Parameters.Add(actionParam);

            var idParam = command.CreateParameter();
            idParam.ParameterName = "@VehicalIdParam";
            idParam.Value = id;
            command.Parameters.Add(idParam);

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "@UserId";
            userIdParam.Value = userId;
            command.Parameters.Add(userIdParam);

            using var reader = await command.ExecuteReaderAsync();

            string json = "";
            if (await reader.ReadAsync())
            {
                if (!reader.IsDBNull(0))
                {
                    json = reader.GetString(0);
                }
            }

            await conn.CloseAsync();

            return string.IsNullOrEmpty(json)
                ? new List<GetVehTransportDTO>()
                : JsonConvert.DeserializeObject<List<GetVehTransportDTO>>(json);
        }



        public async Task AddVehTransportDetail(VehicleTransportData vehicleTransportData)
        {

            var detailTable = ConvertToTransferDetailDataTable(vehicleTransportData.Transfer_Detail);

            var parameters = new[]
            {
                new SqlParameter("@Action", "POST"),
                new SqlParameter("@VehicalId", DBNull.Value),
                new SqlParameter("@Vehical_Number", vehicleTransportData.Vehical_Number),
                new SqlParameter("@Total_Weight", vehicleTransportData.Total_Weight),
                new SqlParameter("@Total_Amount", vehicleTransportData.Total_Amount),
                new SqlParameter("@Laber", vehicleTransportData.Laber),
                new SqlParameter("@Brokerage", vehicleTransportData.Brokerage),
                new SqlParameter("@Market_Shake", vehicleTransportData.Market_Shake),
                new SqlParameter("@Commission", vehicleTransportData.Commission),
                new SqlParameter("@Final_Amount", vehicleTransportData.Final_Amount),
                new SqlParameter("@Total_Count", vehicleTransportData.Total_Count),
                new SqlParameter("@UserId", vehicleTransportData.UserId),
                new SqlParameter
                {
                    ParameterName = "@TransferDetails",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "TransferDetailType",
                    Value = detailTable
                }
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                    "EXEC SP_InsertVehicleWithTransfers @Action, @VehicalId, @Vehical_Number, @Total_Weight, @Total_Amount, @Laber," +
                    " @Brokerage, @Market_Shake, @Commission, @Final_Amount, @Total_Count, @UserId, @TransferDetails",
                    parameters
            );
            await _appDbContext.SaveChangesAsync();

        }

        public async Task UpdateVehTransportDetail(VehicalTransportDataTokenDTO vehicalTransportDataTokenDTO)
        {
            var detailTable = ConvertToTransferDetailDataDTOTable(vehicalTransportDataTokenDTO.Transfer_Detail);

            var parameters = new[]
            {
                new SqlParameter("@Action", "PUT"),
                new SqlParameter("@VehicalId", vehicalTransportDataTokenDTO.VehicalId),
                new SqlParameter("@Vehical_Number", vehicalTransportDataTokenDTO.Vehical_Number ?? (object)DBNull.Value),
                new SqlParameter("@Total_Weight", vehicalTransportDataTokenDTO.Total_Weight),
                new SqlParameter("@Total_Amount", vehicalTransportDataTokenDTO.Total_Amount),
                new SqlParameter("@Laber", vehicalTransportDataTokenDTO.Laber),
                new SqlParameter("@Brokerage", vehicalTransportDataTokenDTO.Brokerage),
                new SqlParameter("@Market_Shake", vehicalTransportDataTokenDTO.Market_Shake),
                new SqlParameter("@Commission", vehicalTransportDataTokenDTO.Commission),
                new SqlParameter("@Final_Amount", vehicalTransportDataTokenDTO.Final_Amount),
                new SqlParameter("@Total_Count", vehicalTransportDataTokenDTO.Total_Count),
                new SqlParameter("@UserId", vehicalTransportDataTokenDTO.UserId),
                new SqlParameter
                {
                    ParameterName = "@TransferDetails",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "TransferDetailType",
                    Value = detailTable
                }
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                "EXEC SP_InsertVehicleWithTransfers " +
                "@Action, @VehicalId, @Vehical_Number, @Total_Weight, @Total_Amount, @Laber, " +
                "@Brokerage, @Market_Shake, @Commission, @Final_Amount, @Total_Count, @UserId, @TransferDetails",
                parameters
            );

            await _appDbContext.SaveChangesAsync();
        }


        public async Task DeleteVehTransportDetail(int id, int userId)
        {
            var table = new DataTable();
            table.Columns.Add("Count", typeof(string));
            table.Columns.Add("Count_Weight", typeof(string));
            table.Columns.Add("Total_Weight", typeof(string));
            table.Columns.Add("Price", typeof(string));
            table.Columns.Add("Total_Amount", typeof(string));

            var parameters = new[]
            {
                new SqlParameter("@Action", "DELETE"),
                new SqlParameter("@VehicalId", id),
               new SqlParameter("@Vehical_Number", DBNull.Value),
                new SqlParameter("@Total_Weight", DBNull.Value),
                new SqlParameter("@Total_Amount", DBNull.Value),
                new SqlParameter("@Laber", DBNull.Value),
                new SqlParameter("@Brokerage", DBNull.Value),
                new SqlParameter("@Market_Shake", DBNull.Value),
                new SqlParameter("@Commission", DBNull.Value),
                new SqlParameter("@Final_Amount", DBNull.Value),
                new SqlParameter("@Total_Count", DBNull.Value),
                new SqlParameter("@UserId", userId),
                new SqlParameter
                {
                    ParameterName = "@TransferDetails",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "TransferDetailType",
                    Value = table
                }
            };

            var result = await _appDbContext.Database.ExecuteSqlRawAsync(
                "EXEC SP_InsertVehicleWithTransfers @Action, @VehicalId, @Vehical_Number, @Total_Weight, @Total_Amount, " +
                "@Laber, @Brokerage, @Market_Shake, @Commission, @Final_Amount, @Total_Count, @UserId, @TransferDetails",
                parameters);

        }

        private DataTable ConvertToTransferDetailDataTable(List<TransferDetail> details)
        {
            var table = new DataTable();
            table.Columns.Add("Count", typeof(string));
            table.Columns.Add("Count_Weight", typeof(string));
            table.Columns.Add("Total_Weight", typeof(string));
            table.Columns.Add("Price", typeof(string));
            table.Columns.Add("Total_Amount", typeof(string));

            foreach (var detail in details)
            {
                table.Rows.Add(
                    detail.Count,
                    detail.Count_Weight,
                    detail.Total_Weight,
                    detail.Price,
                    detail.Total_Amount
                );
            }

            return table;
        }

        private DataTable ConvertToTransferDetailDataDTOTable(List<TransferDetailDto> details)
        {
            var table = new DataTable();
            table.Columns.Add("Count", typeof(string));
            table.Columns.Add("Count_Weight", typeof(string));
            table.Columns.Add("Total_Weight", typeof(string));
            table.Columns.Add("Price", typeof(string));
            table.Columns.Add("Total_Amount", typeof(string));

            foreach (var detail in details)
            {
                table.Rows.Add(
                    detail.Count,
                    detail.Count_Weight,
                    detail.Total_Weight,
                    detail.Price,
                    detail.Total_Amount
                );
            }

            return table;
        }


    }
}
