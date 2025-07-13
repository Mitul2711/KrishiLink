using Microsoft.EntityFrameworkCore;

namespace KrishiLink.DTO.Transport
{
    public class GetTransferDetailDTO
    {
        public int Count { get; set; }

        [Precision(18, 2)]
        public Decimal Count_Weight { get; set; }

        [Precision(18, 2)]
        public Decimal Weight { get; set; }

        [Precision(18, 2)]
        public Decimal Price { get; set; }

        [Precision(18, 2)]
        public Decimal Amount { get; set; }
    }
}
