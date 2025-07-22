using KrishiLink.DTO.Transport;
using KrishiLink.Models.Broker;
using KrishiLink.Models.Farmer;
using KrishiLink.Models.Transport;

namespace KrishiLink.Models.DashBoard
{
    public class DashBoard
    {
        public List<FarmerSale> Farmer { get; set; }
        public List<BrokerData> Broker { get; set; }
        public List<DashVehicalDTO> Transport { get; set; }
    }
}
