using KrishiLink.Models.Transport;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrishiLink.DTO.Transport
{
    public class TransferDetailDto
    {
        public int VehicalId { get; set; }

        public string Count { get; set; }

        public string Count_Weight { get; set; }
            
        public string Weight { get; set; }

        public string Price { get; set; }

        public string Amount { get; set; }
    }
}
