using Microsoft.EntityFrameworkCore;

namespace KrishiLink.DTO.Transport
{
    public class GetVehTransportDTO
    {
        public int VehicalId { get; set; }
        public string Vehical_Number { get; set; }

        public string Total_Weight { get; set; }

        public string Total_Amount { get; set; }
        public string Laber { get; set; }
        public string Brokerage { get; set; }
        public string Market_Shake { get; set; }
        public string Commission { get; set; }

        public string Final_Amount { get; set; }
        public List<GetTransferDetailDTO> Transfer_Detail { get; set; }
    }
}
