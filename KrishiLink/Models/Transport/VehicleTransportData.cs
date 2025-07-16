using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KrishiLink.Models.Transport
{
    public class VehicleTransportData
    {
        [Key]
        public int VehicalId { get; set; }
        public int UserId { get; set; }
        public string Vehical_Number { get; set; }

        public decimal Total_Weight { get; set; }

        public decimal Total_Count { get; set; }

        public decimal Total_Amount { get; set; }
        public decimal Laber { get; set; }
        public decimal Brokerage { get; set; }
        public decimal Market_Shake { get; set; }
        public decimal Commission { get; set; }

        public decimal Final_Amount { get; set; }
        public DateTime Created_At { get; set; } = DateTime.UtcNow;
        public DateTime Updated_At { get; set; } = DateTime.UtcNow;
        public List<TransferDetail> Transfer_Detail { get; set; }
    }
}
