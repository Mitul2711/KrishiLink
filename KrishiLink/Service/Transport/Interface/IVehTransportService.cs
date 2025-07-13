using KrishiLink.DTO.Transport;
using KrishiLink.Models.Transport;

namespace KrishiLink.Service.Transport.Interface
{
    public interface IVehTransportService
    {
        Task<(bool isSuccess, string Message, IEnumerable<GetVehTransportDTO> Data)> GetAllVehTransportDetail();
        Task<(bool isSuccess, string Message, IEnumerable<GetVehTransportDTO> Data)> GetVehTransportDetailById(int id);
        Task<(bool isSuccess, string Message)> AddVehTransportDetail(VehicleTransportDataDTO vehicleTransportDataDTO);
        Task<(bool isSuccess, string Message)> UpdateVehTransportDetail(GetVehTransportDTO getVehTransportDTO);
        Task<(bool IsSuccess, string Message)> DeleteVehTransportDetail(int id);

    }
}
