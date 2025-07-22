namespace KrishiLink.DTO.Transport
{
    public class DashVehicalDTO
    {
        public int VehicalId { get; set; }
        public string Vehical_Number { get; set; }

        public decimal Total_Weight { get; set; }
        public decimal Total_Count { get; set; }

        public decimal Total_Amount { get; set; }
        public decimal Laber { get; set; }
        public decimal Brokerage { get; set; }
        public decimal Market_Shake { get; set; }
        public decimal Commission { get; set; }

        public decimal Final_Amount { get; set; }

        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public List<DashTransportDTO> Transfer_Detail { get; set; }
    }
}