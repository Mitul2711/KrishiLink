using KrishiLink.DTO.Transport;
using KrishiLink.Models.Farmer;
using KrishiLink.Models.Transport;
using KrishiLink.Repository.Transport.Interface;
using KrishiLink.Service.Transport.Interface;

namespace KrishiLink.Service.Transport
{
    public class VehTransportService : IVehTransportService
    {
        private readonly IVehTransportRepository _vehTransportRepository;

        public VehTransportService(IVehTransportRepository vehTransportRepository)
        {
            _vehTransportRepository = vehTransportRepository;
        }

        public async Task<(bool isSuccess, string Message, IEnumerable<GetVehTransportDTO> Data)> GetAllVehTransportDetail()
        {
            var vehicalData = await _vehTransportRepository.GetAllVehTransportDetail();
            if(vehicalData == null || !vehicalData.Any())
            {
                return (false, "Data Not Found", vehicalData);
            }
            else
            {
                return (true, "Transport data retrieved successfully", vehicalData);
            }
        }

        public async Task<(bool isSuccess, string Message, IEnumerable<GetVehTransportDTO> Data)> GetVehTransportDetailById(int id)
        {
            var vehicalData = await _vehTransportRepository.GetVehTransportDetailById(id);
            if (vehicalData == null || !vehicalData.Any())
            {
                return (false, $"No Transport data found with ID = {id}.", null);
            }

            return (true, "Transport data retrieved successfully.", vehicalData);
        }


        public async Task<(bool isSuccess, string Message)> AddVehTransportDetail(VehicleTransportDataDTO vehicleTransportDataDTO)
        {
            if(vehicleTransportDataDTO == null)
            {
                return (false, "Fiels is Empty");
            }

            var transportData = new VehicleTransportData
            {
                Vehical_Number = vehicleTransportDataDTO.Number_Plate,
                Total_Weight = vehicleTransportDataDTO.Total_Weight,
                Total_Amount = vehicleTransportDataDTO.Total_Amount,
                Laber = vehicleTransportDataDTO.Laber,
                Brokerage = vehicleTransportDataDTO.Brokerage,
                Market_Shake = vehicleTransportDataDTO.Market_Shake,
                Commission = vehicleTransportDataDTO.Commission,
                Final_Amount = vehicleTransportDataDTO.Final_Amount,
                Transfer_Detail = vehicleTransportDataDTO.Transfer_Detail
                                    .Select(d => new TransferDetail
                                        {
                                            VehicalId = d.VehicalId,
                                            Count = d.Count,
                                            Count_Weight = d.Count_Weight,
                                            Total_Weight = d.Weight,
                                            Price = d.Price,
                                            Total_Amount = d.Amount
                                        })
                                    .ToList()

            };

            await _vehTransportRepository.AddVehTransportDetail(transportData);
            return (true, "Data Added Successfully!");
        }

        public async Task<(bool isSuccess, string Message)> UpdateVehTransportDetail(GetVehTransportDTO getVehTransportDTO)
        {
            if (getVehTransportDTO == null)
            {
                return (false, "Data is required.");
            }

            var data = await _vehTransportRepository.CheckVehTransportData(getVehTransportDTO.VehicalId);
            if (data == null)
            {
                return (false, "Data Not Found");
            }

            try
            {
                await _vehTransportRepository.UpdateVehTransportDetail(getVehTransportDTO);
                return (true, "Data updated successfully!");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update data: {ex.Message}");
            }
        }

        public async Task<(bool IsSuccess, string Message)> DeleteVehTransportDetail(int id)
        {
            if (id == 0)
            {
                return (false, "Id Is Not Exists!");
            }

            var data = await _vehTransportRepository.CheckVehTransportData(id);
            if (data == null)
            {
                return (false, "Data Not Found");
            }

            try
            {
                await _vehTransportRepository.DeleteVehTransportDetail(id);
                return (true, "Data Deleted");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to Delete data: {ex.Message}");
            }

        }


    }
}
