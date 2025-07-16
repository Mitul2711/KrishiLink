using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KrishiLink.Models.Broker
{
    public class BrokerData
    {
        [Key]
        public int BrokerId { get; set; }
        public int UserId { get; set; }
        public string Broker_name { get; set; }
        public string Mobile { get; set; }
        public string Village { get; set; }
        public string Crop_Name { get; set; }
        public string Crop_Type { get; set; }

        public decimal Weight { get; set; }

        public decimal Price { get; set; }

        public decimal Count { get; set; }
        public decimal Total_Amount { get; set; }

        public decimal Total_Brokerage { get; set; }

        public DateTime Created_At { get; set; } = DateTime.UtcNow;
        public DateTime Updated_At { get; set; } = DateTime.UtcNow;
    }
}
