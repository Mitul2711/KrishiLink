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

        public int VehicalId { get; set; } // Foreign key

        [ForeignKey("VehicalId")]
        public VehicleTransportData VehicleTransportData { get; set; }

        public int Count { get; set; }

        [Precision(18, 2)]
        public Decimal Count_Weight { get; set; }
        
        [Precision(18, 2)]
        public Decimal Weight { get; set; }

        [Precision(18, 2)]
        public Decimal Price { get; set; }

        [Precision(18, 2)]
        public Decimal Amount { get; set; }
    }
}
