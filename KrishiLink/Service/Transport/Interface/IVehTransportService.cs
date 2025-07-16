using KrishiLink.DTO.Transport;

namespace KrishiLink.Service.Transport.Interface
{
    public interface IVehTransportService
    {
        Task<(string status_code, string status_message, IEnumerable<GetVehTransportDTO> Data)> GetAllVehTransportDetail(int userId, string access_token);
        Task<(string status_code, string status_message, IEnumerable<GetVehTransportDTO> Data)> GetVehTransportDetailById(int id, int userId, string access_token);
        Task<(string status_code, string status_message)> AddVehTransportDetail(VehicleTransportDataDTO vehicleTransportDataDTO);
        Task<(string status_code, string status_message)> UpdateVehTransportDetail(VehicalTransportDataTokenDTO vehicalTransportDataTokenDTO);
        Task<(string status_code, string status_message)> DeleteVehTransportDetail(int id, int userId, string access_token);

    }
}
