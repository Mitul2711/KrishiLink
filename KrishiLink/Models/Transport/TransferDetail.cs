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

        public string Count { get; set; }

        public string Count_Weight { get; set; }
        
        public string Total_Weight { get; set; }

        public string Price { get; set; }

        public string Total_Amount { get; set; }
    }
}
