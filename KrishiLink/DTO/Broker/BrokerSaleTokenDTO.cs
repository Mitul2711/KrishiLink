namespace KrishiLink.DTO.Broker
{
    public class BrokerSaleTokenDTO
    {
        public int BrokerId { get; set; }
        public int UserId { get; set; }
        public string AccessToken { get; set; }
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
    }
}
