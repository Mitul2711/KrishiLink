using KrishiLink.DTO.Farmer;
using KrishiLink.Models.Farmer;
using KrishiLink.Services.Farmer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KrishiLink.Controllers.Farmer
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FarmerSaleController : ControllerBase
    {
        private readonly IFarmerSaleService _farmerSaleService;

        public FarmerSaleController(IFarmerSaleService farmerSaleService)
        {
            _farmerSaleService = farmerSaleService;
        }

        [HttpGet("GetAllFarmerSaleDetail")]
        public async Task<IActionResult> GetAllFarmerSaleDetails()
        {
            var (IsSuccess, Message, Data) = await _farmerSaleService.GetAllFarmerSaleDetails();
            if (!IsSuccess)
            {
                return NotFound(new { IsSuccess, Message });
            }
            else
            {
                return Ok(new { IsSuccess, Message, Data });
            }
        }

        [HttpGet("GetFarmerSaleDetailById")]
        public async Task<IActionResult> GetFarmerSaleDetailsById(int id)
        {
            var (IsSuccess, Message, Data) = await _farmerSaleService.GetFarmerSaleDetailsById(id);
            if (!IsSuccess)
            {
                return NotFound(new { IsSuccess, Message });
            }
            else
            {
                return Ok(new { IsSuccess, Message, Data });
            }
        }

        [HttpPost("AddFarmerSaleDetail")]
        public async Task<ActionResult> AddFarmerSaleDetails(FarmerSaleDTO farmerSaleDTO)
        {
            var (IsSuccess, Message) = await _farmerSaleService.AddFarmerSaleDetails(farmerSaleDTO);
            if (!IsSuccess)
            {
                return NotFound(new { IsSuccess, Message });
            }
            else
            {
                return Ok(new { IsSuccess, Message });
            }
        }

        [HttpPut("UpdateFarmerSaleDetail")]
        public async Task<ActionResult> UpdateFarmerSaleDetails(FarmerSale farmerSale)
        {
            var (IsSuccess, Message) = await _farmerSaleService.UpdateFarmerSaleDetails(farmerSale);
            if (!IsSuccess)
            {
                return NotFound(new { IsSuccess, Message });
            }
            else
            {
                return Ok(new { IsSuccess, Message });
            }
        }

        [HttpDelete("DeleteFarmerSaleDetail")]
        public async Task<ActionResult> DeleteFarmerSaleDetails(int id)
        {
            var (IsSuccess, Message) = await _farmerSaleService.DeleteFarmerSaleDetails(id);
            if (!IsSuccess)
            {
                return NotFound(new { IsSuccess, Message });
            }
            else
            {
                return Ok(new { IsSuccess, Message });
            }
        }
    }
}
