using KrishiLink.DBContext;
using KrishiLink.DTO.Transport;
using KrishiLink.Models.Farmer;
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
        public async Task<IEnumerable<GetVehTransportDTO>> GetAllVehTransportDetail()
        {
            using var conn = _appDbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var command = conn.CreateCommand();
            command.CommandText = "EXEC SP_InsertVehicleWithTransfers @Action = 'GET'";
            using var reader = await command.ExecuteReaderAsync();

            string json = "";
            if (await reader.ReadAsync())
            {
                json = reader.GetString(0);
            }

            await conn.CloseAsync();

            return JsonConvert.DeserializeObject<List<GetVehTransportDTO>>(json);
        }

        public async Task<IEnumerable<GetVehTransportDTO>> GetVehTransportDetailById(int id)
        {
            using var conn = _appDbContext.Database.GetDbConnection();
            await conn.OpenAsync();

            using var command = conn.CreateCommand();
            command.CommandText = "EXEC SP_InsertVehicleWithTransfers @Action = @ActionParam, @VehicalId = @VehicalIdParam";

            var actionParam = command.CreateParameter();
            actionParam.ParameterName = "@ActionParam";
            actionParam.Value = "GET";
            command.Parameters.Add(actionParam);

            var idParam = command.CreateParameter();
            idParam.ParameterName = "@VehicalIdParam";
            idParam.Value = id;
            command.Parameters.Add(idParam);

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
                new SqlParameter("@Vehical_Number", vehicleTransportData.Vehical_Number ?? (object)DBNull.Value),
                new SqlParameter("@Total_Weight", vehicleTransportData.Total_Weight),
                new SqlParameter("@Total_Amount", vehicleTransportData.Total_Amount),
                new SqlParameter("@Laber", vehicleTransportData.Laber ?? (object)DBNull.Value),
                new SqlParameter("@Brokerage", vehicleTransportData.Brokerage?? (object)DBNull.Value),
                new SqlParameter("@Market_Shake", vehicleTransportData.Market_Shake ?? (object)DBNull.Value),
                new SqlParameter("@Commission", vehicleTransportData.Commission ?? (object)DBNull.Value),
                new SqlParameter("@Final_Amount", vehicleTransportData.Final_Amount),
                new SqlParameter
                {
                    ParameterName = "@TransferDetails",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "TransferDetailType",
                    Value = detailTable
                }
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                    "EXEC SP_InsertVehicleWithTransfers @Action, @VehicalId, @Number_Plate, @Total_Weight, @Total_Amount, @Laber, @Brokerage, @Market_Shake, @Commission, @Final_Amount, @TransferDetails",
                    parameters
            );
            await _appDbContext.SaveChangesAsync();

        }

        public async Task UpdateVehTransportDetail(GetVehTransportDTO getVehTransportDTO)
        {

            var detailTable = ConvertToTransferDetailDataDTOTable(getVehTransportDTO.Transfer_Detail);

            var parameters = new[]
            {
                new SqlParameter("@Action", "PUT"),
                new SqlParameter("@VehicalId", getVehTransportDTO.VehicalId),
                new SqlParameter("@Number_Plate", getVehTransportDTO.Vehical_Number ?? (object)DBNull.Value),
                new SqlParameter("@Total_Weight", getVehTransportDTO.Total_Weight),
                new SqlParameter("@Total_Amount", getVehTransportDTO.Total_Amount),
                new SqlParameter("@Laber", getVehTransportDTO.Laber ?? (object)DBNull.Value),
                new SqlParameter("@Brokerage", getVehTransportDTO.Brokerage?? (object)DBNull.Value),
                new SqlParameter("@Market_Shake", getVehTransportDTO.Market_Shake ?? (object)DBNull.Value),
                new SqlParameter("@Commission", getVehTransportDTO.Commission ?? (object)DBNull.Value),
                new SqlParameter("@Final_Amount", getVehTransportDTO.Final_Amount),
                new SqlParameter
                {
                    ParameterName = "@TransferDetails",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "TransferDetailType",
                    Value = detailTable
                }
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                    "EXEC SP_InsertVehicleWithTransfers @Action, @VehicalId, @Number_Plate, @Total_Weight, @Total_Amount, @Laber, @Brokerage, @Market_Shake, @Commission, @Final_Amount, @TransferDetails",
                    parameters
            );
            await _appDbContext.SaveChangesAsync();

        }

        public async Task DeleteVehTransportDetail(int id)
        {
            var table = new DataTable();
            table.Columns.Add("Count", typeof(int));
            table.Columns.Add("Count_Weight", typeof(decimal));
            table.Columns.Add("Weight", typeof(decimal));
            table.Columns.Add("Price", typeof(decimal));
            table.Columns.Add("Amount", typeof(decimal));

            var parameters = new[]
            {
                new SqlParameter("@Action", "DELETE"),
                new SqlParameter("@VehicalId", id),
                new SqlParameter("@Number_Plate", DBNull.Value),
                new SqlParameter("@Total_Weight", DBNull.Value),
                new SqlParameter("@Total_Amount", DBNull.Value),
                new SqlParameter("@Laber", DBNull.Value),
                new SqlParameter("@Brokerage", DBNull.Value),
                new SqlParameter("@Market_Shake", DBNull.Value),
                new SqlParameter("@Commission", DBNull.Value),
                new SqlParameter("@Final_Amount", DBNull.Value),
                new SqlParameter
                {
                    ParameterName = "@TransferDetails",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "TransferDetailType",
                    Value = table
                }
            };

            var result = await _appDbContext.Database.ExecuteSqlRawAsync(
                "EXEC SP_InsertVehicleWithTransfers @Action, @VehicalId, @Number_Plate, @Total_Weight, @Total_Amount, @Laber, @Brokerage, @Market_Shake, @Commission, @Final_Amount, @TransferDetails",
                parameters);

        }

        private DataTable ConvertToTransferDetailDataTable(List<TransferDetail> details)
        {
            var table = new DataTable();
            table.Columns.Add("Count", typeof(int));
            table.Columns.Add("Count_Weight", typeof(decimal));
            table.Columns.Add("Weight", typeof(decimal));
            table.Columns.Add("Price", typeof(decimal));
            table.Columns.Add("Amount", typeof(decimal));

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

        private DataTable ConvertToTransferDetailDataDTOTable(List<GetTransferDetailDTO> details)
        {
            var table = new DataTable();
            table.Columns.Add("Count", typeof(int));
            table.Columns.Add("Count_Weight", typeof(decimal));
            table.Columns.Add("Weight", typeof(decimal));
            table.Columns.Add("Price", typeof(decimal));
            table.Columns.Add("Amount", typeof(decimal));

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
