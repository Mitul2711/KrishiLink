using KrishiLink.DTO.Broker;
using KrishiLink.Models.Broker;
using KrishiLink.Repository.Auth.Interface;
using KrishiLink.Repository.Broker.Interface;
using KrishiLink.Service.Broker.Interface;

namespace KrishiLink.Service.Broker
{
    public class BrokerDataService : IBrokerDataService
    {
        private readonly IBrokerDataRepository _brokerDataRepository;

        public IUserRepository _UserRepository { get; }

        public BrokerDataService(IBrokerDataRepository brokerDataRepository, IUserRepository userRepository)
        {
            _brokerDataRepository = brokerDataRepository;
            _UserRepository = userRepository;
        }


        public async Task<(string status_code, string status_message, IEnumerable<BrokerData> Data)> GetAllBrokerData(int userId, string access_token)
        {
            var result = await _brokerDataRepository.GetAllBrokerData(userId, access_token);
            if (result == null || !result.Any())
            {
                return ("0", "No Broker data found.", null);
            }

            return ("1", "Broker data retrieved successfully.", result);
        }

        public async Task<(string status_code, string status_message, BrokerData Data)> GetBrokerDataById(int id, int userId, string access_token)
        {
            var result = await _brokerDataRepository.GetBrokerDataById(id, userId, access_token);
            if (result == null)
            {
                return ("0", $"No Broker Data found with ID = {id}.", null);
            }

            return ("1", "Broker data retrieved successfully.", result);
        }

        public async Task<(string status_code, string status_message)> AddBrokerData(BrokerDataDTO brokerDataDTO)
        {
            var isAccess = await _UserRepository.CheckAccess(brokerDataDTO.UserId, brokerDataDTO.AccessToken);
            if (isAccess == null)
            {
                return ("0", "Session Expired!");
            }

            if (brokerDataDTO == null)
            {
                return ("0", "Data is required.");
            }

            try
            {

                await _brokerDataRepository.AddBrokerData(brokerDataDTO);
                return ("1", "Broker Data added successfully!");
            }
            catch (Exception ex)
            {
                return ("0", $"Failed to add data: {ex.Message}");
            }
        }

        public async Task<(string status_code, string status_message)> UpdateBrokerData(BrokerSaleTokenDTO brokerSaleTokenDTO)
        {
            var isAccess = await _UserRepository.CheckAccess(brokerSaleTokenDTO.UserId, brokerSaleTokenDTO.AccessToken);
            if (isAccess == null)
            {
                return ("0", "Session Expired!");

            }
            if (brokerSaleTokenDTO == null)
            {
                return ("0", "Data is required.");
            }

            var data = await _brokerDataRepository.GetBrokerDataById(brokerSaleTokenDTO.BrokerId, brokerSaleTokenDTO.UserId, brokerSaleTokenDTO.AccessToken);
            if (data == null)
            {
                return ("0", "Data Not Found");
            }

            try
            {
                await _brokerDataRepository.UpdateBrokerData(brokerSaleTokenDTO);
                return ("1", "Data updated successfully!");
            }
            catch (Exception ex)
            {
                return ("0", $"Failed to update data: {ex.Message}");
            }
        }

        public async Task<(string status_code, string status_message)> DeleteBrokerData(int id, int userId, string access_token)
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

            var data = await _brokerDataRepository.GetBrokerDataById(id, userId, access_token);
            if (data == null)
            {
                return ("0", "Data Not Found");
            }

            try
            {
                await _brokerDataRepository.DeleteBrokerData(id, userId);
                return ("1", "Data Deleted");
            }
            catch (Exception ex)
            {
                return ("0", $"Failed to Delete data: {ex.Message}");
            }

        }

    }
}
