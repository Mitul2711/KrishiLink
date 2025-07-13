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

        public string Weight { get; set; }

        public string Price { get; set; }

        public string Count { get; set; }
        public string Total_Amount { get; set; }

        public string Total_Brokerage { get; set; }

        public DateTime Created_At { get; set; } = DateTime.UtcNow;
        public DateTime Updated_At { get; set; } = DateTime.UtcNow;
    }
}
