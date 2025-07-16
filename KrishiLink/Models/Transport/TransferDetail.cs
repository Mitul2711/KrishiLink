using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KrishiLink.Models.Transport
{
    public class TransferDetail
    {
        [Key]
        public int TransferId { get; set; }

        public int VehicalId { get; set; }

        [ForeignKey("VehicalId")]
        public VehicleTransportData VehicleTransportData { get; set; }
        public decimal Count { get; set; }

        public decimal Count_Weight { get; set; }

        public decimal Total_Weight { get; set; }

        public decimal Price { get; set; }

        public decimal Total_Amount { get; set; }
    }
}
