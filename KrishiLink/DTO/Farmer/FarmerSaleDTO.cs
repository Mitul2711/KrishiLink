using Microsoft.EntityFrameworkCore;

namespace KrishiLink.DTO.Farmer
{
    public class FarmerSaleDTO
    {
        public string Farmer_name { get; set; }
        public string Mobile { get; set; }
        public string Village { get; set; }
        public string Crop_Name { get; set; }
        public string Crop_Type { get; set; }

        [Precision(18, 2)]
        public Decimal Weight { get; set; }

        [Precision(18, 2)]
        public Decimal Price { get; set; }
    }
}
