using KrishiLink.Models.Transport;
using Microsoft.EntityFrameworkCore;

namespace KrishiLink.DTO.Transport
{
    public class VehicleTransportDataDTO
    {
        public int UserId { get; set; }
        public string AccessToken { get; set; }
        public string Vehical_Number { get; set; }

        public decimal Total_Weight { get; set; }

        public decimal Total_Amount { get; set; }
        public decimal Laber { get; set; }
        public decimal Brokerage { get; set; }
        public decimal Market_Shake { get; set; }
        public decimal Commission { get; set; }

        public decimal Final_Amount { get; set; }
        public decimal Total_Count { get; set; }
        public List<TransferDetailDto> Transfer_Detail { get; set; }
    }
}
