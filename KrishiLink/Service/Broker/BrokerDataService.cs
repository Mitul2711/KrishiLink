using KrishiLink.DTO.Broker;
using KrishiLink.DTO.Farmer;
using KrishiLink.Models.Broker;
using KrishiLink.Models.Farmer;
using KrishiLink.Repository.Broker.Interface;
using KrishiLink.Service.Broker.Interface;

namespace KrishiLink.Service.Broker
{
    public class BrokerDataService : IBrokerDataService
    {
        private readonly IBrokerDataRepository _brokerDataRepository;

        public BrokerDataService(IBrokerDataRepository brokerDataRepository)
        {
            _brokerDataRepository = brokerDataRepository;
        }


        public async Task<(bool IsSuccess, string Message, IEnumerable<BrokerData> Data)> GetAllBrokerData()
        {
            var result = await _brokerDataRepository.GetAllBrokerData();
            if (result == null || !result.Any())
            {
                return (false, "No Broker data found.", null);
            }

            return (true, "Broker data retrieved successfully.", result);
        }

        public async Task<(bool IsSuccess, string Message, BrokerData Data)> GetBrokerDataById(int id)
        {
            var result = await _brokerDataRepository.GetBrokerDataById(id);
            if (result == null)
            {
                return (false, $"No Broker Data found with ID = {id}.", null);
            }

            return (true, "Broker data retrieved successfully.", result);
        }

        public async Task<(bool IsSuccess, string Message)> AddBrokerData(BrokerDataDTO brokerDataDTO)
        {
            if (brokerDataDTO == null)
            {
                return (false, "Data is required.");
            }

            try
            {
                var brokerDetails = new BrokerData
                {
                    Broker_name = brokerDataDTO.Broker_name,
                    Mobile = brokerDataDTO.Mobile,
                    Village = brokerDataDTO.Village,
                    Crop_Name = brokerDataDTO.Crop_Name,
                    Crop_Type = brokerDataDTO.Crop_Type,
                    Weight = brokerDataDTO.Weight,
                    Price = brokerDataDTO.Price
                };

                await _brokerDataRepository.AddBrokerData(brokerDetails);
                return (true, "Broker Data added successfully!");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to add data: {ex.Message}");
            }
        }

        public async Task<(bool IsSuccess, string Message)> UpdateBrokerData(BrokerData brokerData)
        {
            if (brokerData == null)
            {
                return (false, "Data is required.");
            }

            var data = await _brokerDataRepository.GetBrokerDataById(brokerData.BrokerId);
            if (data == null)
            {
                return (false, "Data Not Found");
            }

            try
            {
                await _brokerDataRepository.UpdateBrokerData(brokerData);
                return (true, "Data updated successfully!");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update data: {ex.Message}");
            }
        }

        public async Task<(bool IsSuccess, string Message)> DeleteBrokerData(int id)
        {
            if (id == 0)
            {
                return (false, "Id Is Not Exists!");
            }

            var data = await _brokerDataRepository.GetBrokerDataById(id);
            if (data == null)
            {
                return (false, "Data Not Found");
            }

            try
            {
                await _brokerDataRepository.DeleteBrokerData(id);
                return (true, "Data Deleted");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to Delete data: {ex.Message}");
            }

        }

    }
}
