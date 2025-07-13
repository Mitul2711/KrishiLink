using Microsoft.EntityFrameworkCore;

namespace KrishiLink.DTO.Transport
{
    public class GetTransferDetailDTO
    {
        public int Count { get; set; }

        public string Count_Weight { get; set; }

        public string Total_Weight { get; set; }

        public string Price { get; set; }

        public string Total_Amount { get; set; }
    }
}
