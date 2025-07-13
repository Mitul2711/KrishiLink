using Microsoft.EntityFrameworkCore;

namespace KrishiLink.DTO.Transport
{
    public class GetVehTransportDTO
    {
        public int VehicalId { get; set; }
        public string Number_Plate { get; set; }

        [Precision(18, 2)]
        public Decimal Total_Weight { get; set; }

        [Precision(18, 2)]
        public Decimal Total_Amount { get; set; }
        public string Laber { get; set; }
        public string Brokerage { get; set; }
        public string Market_Shake { get; set; }
        public string Commission { get; set; }

        [Precision(18, 2)]
        public Decimal Final_Amount { get; set; }
        public List<GetTransferDetailDTO> Transfer_Detail { get; set; }
    }
}
