using KrishiLink.Models.Broker;
using KrishiLink.Models.Farmer;

namespace KrishiLink.Repository.Broker.Interface
{
    public interface IBrokerDataRepository
    {
        public Task<IEnumerable<BrokerData>> GetAllBrokerData();
        public Task<BrokerData> GetBrokerDataById(int id);

        public Task AddBrokerData(BrokerData brokerData);
        public Task UpdateBrokerData(BrokerData brokerData);

        public Task DeleteBrokerData(int id);
    }
}
