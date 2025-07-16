using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KrishiLink.Models.Farmer
{
    public class FarmerSale
    {
        [Key]
        public int FarmerId { get; set; }
        public int UserId { get; set; }
        public string Farmer_name { get; set; }
        public string Mobile { get; set; }
        public string Village { get; set; }
        public string Crop_Name { get; set; }
        public string Crop_Type { get; set; }

        public decimal Weight { get; set; }

        public decimal Price { get; set; }

        public decimal Total_Price { get; set; }

        public DateTime Created_At { get; set; } = DateTime.UtcNow;
        public DateTime Updated_At { get; set; } = DateTime.UtcNow;

    }
}
