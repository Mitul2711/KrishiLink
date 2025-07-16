using KrishiLink.DTO.Broker;
using KrishiLink.Models.Broker;

namespace KrishiLink.Repository.Broker.Interface
{
    public interface IBrokerDataRepository
    {
        public Task<IEnumerable<BrokerData>> GetAllBrokerData(int userId, string access_token);
        public Task<BrokerData> GetBrokerDataById(int id, int userId, string access_token);

        public Task AddBrokerData(BrokerDataDTO brokerDataDTO);
        public Task UpdateBrokerData(BrokerSaleTokenDTO brokerSaleTokenDTO);

        public Task DeleteBrokerData(int id, int userId);
    }
}
