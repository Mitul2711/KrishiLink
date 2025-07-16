using KrishiLink.DTO.Transport;
using KrishiLink.Models.Transport;
using KrishiLink.Repository.Auth.Interface;
using KrishiLink.Repository.Transport.Interface;
using KrishiLink.Service.Transport.Interface;

namespace KrishiLink.Service.Transport
{
    public class VehTransportService : IVehTransportService
    {
        private readonly IVehTransportRepository _vehTransportRepository;

        public IUserRepository _UserRepository { get; }

        public VehTransportService(IVehTransportRepository vehTransportRepository, IUserRepository userRepository)
        {
            _vehTransportRepository = vehTransportRepository;
            _UserRepository = userRepository;
        }

        public async Task<(string status_code, string status_message, IEnumerable<GetVehTransportDTO> Data)> GetAllVehTransportDetail(int userId, string access_token)
        {
            var isAccess = await _UserRepository.CheckAccess(userId, access_token);
            if (isAccess == null)
            {
                return ("0", "Session Expired!", null);
            }

            var vehicalData = await _vehTransportRepository.GetAllVehTransportDetail(userId);
            if (vehicalData == null || !vehicalData.Any())
            {
                return ("0", "Data Not Found", vehicalData);
            }
            else
            {
                return ("1", "Transport data retrieved successfully", vehicalData);
            }
        }

        public async Task<(string status_code, string status_message, IEnumerable<GetVehTransportDTO> Data)> GetVehTransportDetailById(int id, int userId, string access_token)
        {
            var isAccess = await _UserRepository.CheckAccess(userId, access_token);
            if (isAccess == null)
            {
                return ("0", "Session Expired!", null);
            }

            var vehicalData = await _vehTransportRepository.GetVehTransportDetailById(id, userId);
            if (vehicalData == null || !vehicalData.Any())
            {
                return ("0", $"No Transport data found with ID = {id}.", null);
            }

            return ("1", "Transport data retrieved successfully.", vehicalData);
        }


        public async Task<(string status_code, string status_message)> AddVehTransportDetail(VehicleTransportDataDTO vehicleTransportDataDTO)
        {
            var isAccess = await _UserRepository.CheckAccess(vehicleTransportDataDTO.UserId, vehicleTransportDataDTO.AccessToken);
            if (isAccess == null)
            {
                return ("0", "Session Expired!");
            }

            if (vehicleTransportDataDTO == null)
            {
                return ("0", "Fiels is Empty");
            }

            var transportData = new VehicleTransportData
            {
                UserId = vehicleTransportDataDTO.UserId,
                Vehical_Number = vehicleTransportDataDTO.Vehical_Number,
                Total_Weight = vehicleTransportDataDTO.Total_Weight,
                Total_Amount = vehicleTransportDataDTO.Total_Amount,
                Total_Count = vehicleTransportDataDTO.Total_Count,
                Laber = vehicleTransportDataDTO.Laber,
                Brokerage = vehicleTransportDataDTO.Brokerage,
                Market_Shake = vehicleTransportDataDTO.Market_Shake,
                Commission = vehicleTransportDataDTO.Commission,
                Final_Amount = vehicleTransportDataDTO.Final_Amount,
                Transfer_Detail = vehicleTransportDataDTO.Transfer_Detail
                                    .Select(d => new TransferDetail
                                    {
                                        Count = d.Count,
                                        Count_Weight = d.Count_Weight,
                                        Total_Weight = d.Total_Weight,
                                        Price = d.Price,
                                        Total_Amount = d.Total_Amount
                                    })
                                    .ToList()

            };

            await _vehTransportRepository.AddVehTransportDetail(transportData);
            return ("1", "Data Added Successfully!");
        }

        public async Task<(string status_code, string status_message)> UpdateVehTransportDetail(VehicalTransportDataTokenDTO vehicalTransportDataTokenDTO)
        {
            if (vehicalTransportDataTokenDTO == null)
            {
                return ("0", "Data is required.");
            }

            var data = await _vehTransportRepository.CheckVehTransportData(vehicalTransportDataTokenDTO.VehicalId);
            if (data == null)
            {
                return ("0", "Data Not Found");
            }

            try
            {
                await _vehTransportRepository.UpdateVehTransportDetail(vehicalTransportDataTokenDTO);
                return ("1", "Data updated successfully!");
            }
            catch (Exception ex)
            {
                return ("0", $"Failed to update data: {ex.Message}");
            }
        }

        public async Task<(string status_code, string status_message)> DeleteVehTransportDetail(int id, int userId, string access_token)
        {
            var isAccess = await _UserRepository.CheckAccess(userId, access_token);
            if (isAccess == null)
            {
                return ("0", "Session Expired!");
            }

            if (id == 0)
            {
                return ("0", "Id Is Not Exists!");
            }

            var data = await _vehTransportRepository.CheckVehTransportData(id);
            if (data == null)
            {
                return ("0", "Data Not Found");
            }

            try
            {
                await _vehTransportRepository.DeleteVehTransportDetail(id, userId);
                return ("1", "Data Deleted");
            }
            catch (Exception ex)
            {
                return ("0", $"Failed to Delete data: {ex.Message}");
            }

        }


    }
}
