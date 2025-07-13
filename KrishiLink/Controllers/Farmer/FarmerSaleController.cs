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

        [HttpPost("GetAllFarmerSaleDetail")]
        public async Task<IActionResult> GetAllFarmerSaleDetails(int userId, string access_token)
        {
            var (status_code, Message, Data) = await _farmerSaleService.GetAllFarmerSaleDetails(userId, access_token);
            if (status_code == "0")
            {
                return NotFound(new { status_code, Message });
            }
            else
            {
                return Ok(new { status_code, Message, Data });
            }
        }

        [HttpPost("GetFarmerSaleDetailById")]
        public async Task<IActionResult> GetFarmerSaleDetailsById(int id, int userId, string access_token)
        {
            var (status_code, Message, Data) = await _farmerSaleService.GetFarmerSaleDetailsById(id, userId, access_token);
            if (status_code == "0")
            {
                return NotFound(new { status_code, Message });
            }
            else
            {
                return Ok(new { status_code, Message, Data });
            }
        }

        [HttpPost("AddFarmerSaleDetail")]
        public async Task<ActionResult> AddFarmerSaleDetails(FarmerSaleDTO farmerSaleDTO)
        {
            var (status_code, Message) = await _farmerSaleService.AddFarmerSaleDetails(farmerSaleDTO);
            if (status_code == "0")
            {
                return NotFound(new { status_code, Message });
            }
            else
            {
                return Ok(new { status_code, Message });
            }
        }

        [HttpPost("UpdateFarmerSaleDetail")]
        public async Task<ActionResult> UpdateFarmerSaleDetails(FarmerSaleTokenDTO farmerSaleTokenDTO)
        {
            var (status_code, Message) = await _farmerSaleService.UpdateFarmerSaleDetails(farmerSaleTokenDTO);
            if (status_code == "0")
            {
                return NotFound(new { status_code, Message });
            }
            else
            {
                return Ok(new { status_code, Message });
            }
        }

        [HttpPost("DeleteFarmerSaleDetail")]
        public async Task<ActionResult> DeleteFarmerSaleDetails(int id, int userId, string accessToken)
        {
            var (status_code, Message) = await _farmerSaleService.DeleteFarmerSaleDetails(id, userId, accessToken);
            if (status_code == "0")
            {
                return NotFound(new { status_code, Message });
            }
            else
            {
                return Ok(new { status_code, Message });
            }
        }
    }
}
