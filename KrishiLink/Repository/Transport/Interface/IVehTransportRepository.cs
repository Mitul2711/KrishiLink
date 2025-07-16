using KrishiLink.DTO.Transport;
using KrishiLink.Models.Transport;

namespace KrishiLink.Repository.Transport.Interface
{
    public interface IVehTransportRepository
    {
        Task<IEnumerable<GetVehTransportDTO>> GetAllVehTransportDetail(int userId);
        Task<IEnumerable<GetVehTransportDTO>> GetVehTransportDetailById(int id, int userId);
        Task<CheckVehTransportDTO> CheckVehTransportData(int id);
        Task AddVehTransportDetail(VehicleTransportData vehicleTransportData);
        Task UpdateVehTransportDetail(VehicalTransportDataTokenDTO vehicalTransportDataTokenDTO);
        Task DeleteVehTransportDetail(int id, int userId);
    }
}
