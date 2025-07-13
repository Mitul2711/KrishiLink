using KrishiLink.Models.Transport;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrishiLink.DTO.Transport
{
    public class TransferDetailDto
    {
        public int VehicalId { get; set; }

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
