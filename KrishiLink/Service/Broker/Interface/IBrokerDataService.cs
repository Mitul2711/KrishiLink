using KrishiLink.DTO.Broker;
using KrishiLink.Models.Broker;

namespace KrishiLink.Service.Broker.Interface
{
    public interface IBrokerDataService
    {
        Task<(string status_code, string status_message, IEnumerable<BrokerData> Data)> GetAllBrokerData(int userId, string access_token);
        Task<(string status_code, string status_message, BrokerData Data)> GetBrokerDataById(int id, int userId, string access_token);

        Task<(string status_code, string status_message)> AddBrokerData(BrokerDataDTO brokerDataDTO);
        Task<(string status_code, string status_message)> UpdateBrokerData(BrokerSaleTokenDTO brokerSaleTokenDTO);
        Task<(string status_code, string status_message)> DeleteBrokerData(int id, int userId, string access_token);
    }
}
