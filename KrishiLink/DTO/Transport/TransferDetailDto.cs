using KrishiLink.Models.Transport;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrishiLink.DTO.Transport
{
    public class TransferDetailDto
    {
        public decimal Count { get; set; }

        public decimal Count_Weight { get; set; }

        public decimal Total_Weight { get; set; }

        public decimal Price { get; set; }

        public decimal Total_Amount { get; set; }
    }
}
