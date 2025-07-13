using KrishiLink.DTO.Transport;
using KrishiLink.Models.Transport;

namespace KrishiLink.Repository.Transport.Interface
{
    public interface IVehTransportRepository
    {
        Task<IEnumerable<GetVehTransportDTO>> GetAllVehTransportDetail();
        Task<IEnumerable<GetVehTransportDTO>> GetVehTransportDetailById(int id);
        Task<CheckVehTransportDTO> CheckVehTransportData(int id);
        Task AddVehTransportDetail(VehicleTransportData vehicleTransportData);
        Task UpdateVehTransportDetail(GetVehTransportDTO getVehTransportDTO);
        Task DeleteVehTransportDetail(int id);
    }
}
