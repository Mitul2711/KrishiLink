namespace KrishiLink.DTO.Farmer
{
    public class FarmerSaleTokenDTO
    {
        public int FarmerId { get; set; }
        public int UserId { get; set; }
        public string Access_Token { get; set; }
        public string Farmer_name { get; set; }
        public string Mobile { get; set; }
        public string Village { get; set; }
        public string Crop_Name { get; set; }
        public string Crop_Type { get; set; }

        public string Weight { get; set; }

        public string Price { get; set; }
        public string Total_Price { get; set; }
    }
}
