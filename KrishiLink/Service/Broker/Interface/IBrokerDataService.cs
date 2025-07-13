using KrishiLink.DTO.Broker;
using KrishiLink.DTO.Farmer;
using KrishiLink.Models.Broker;
using KrishiLink.Models.Farmer;

namespace KrishiLink.Service.Broker.Interface
{
    public interface IBrokerDataService
    {
        Task<(bool IsSuccess, string Message, IEnumerable<BrokerData> Data)> GetAllBrokerData();
        Task<(bool IsSuccess, string Message, BrokerData Data)> GetBrokerDataById(int id);

        Task<(bool IsSuccess, string Message)> AddBrokerData(BrokerDataDTO brokerDataDTO);
        Task<(bool IsSuccess, string Message)> UpdateBrokerData(BrokerData brokerData);
        Task<(bool IsSuccess, string Message)> DeleteBrokerData(int id);
    }
}
