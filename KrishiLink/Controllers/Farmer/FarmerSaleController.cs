using KrishiLink.DTO.Access_Token;
using KrishiLink.DTO.Farmer;
using KrishiLink.Services.Farmer.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KrishiLink.Controllers.Farmer
{
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
        public async Task<IActionResult> GetAllFarmerSaleDetails(Access_TokenDTO access_TokenDTO)
        {
            var (status_code, status_message, Data) = await _farmerSaleService.GetAllFarmerSaleDetails(access_TokenDTO.UserId, access_TokenDTO.AccessToken);
            if (status_code == "0")
            {
                return Ok(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message, Data });
            }
        }

        [HttpPost("GetFarmerSaleDetailById")]
        public async Task<IActionResult> GetFarmerSaleDetailsById(int id, int userId, string access_token)
        {
            var (status_code, status_message, Data) = await _farmerSaleService.GetFarmerSaleDetailsById(id, userId, access_token);
            if (status_code == "0")
            {
                return Ok(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message, Data });
            }
        }

        [HttpPost("AddFarmerSaleDetail")]
        public async Task<ActionResult> AddFarmerSaleDetails(FarmerSaleDTO farmerSaleDTO)
        {
            var (status_code, status_message) = await _farmerSaleService.AddFarmerSaleDetails(farmerSaleDTO);
            if (status_code == "0")
            {
                return Ok(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message });
            }
        }

        [HttpPost("UpdateFarmerSaleDetail")]
        public async Task<ActionResult> UpdateFarmerSaleDetails(FarmerSaleTokenDTO farmerSaleTokenDTO)
        {
            var (status_code, status_message) = await _farmerSaleService.UpdateFarmerSaleDetails(farmerSaleTokenDTO);
            if (status_code == "0")
            {
                return Ok(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message });
            }
        }

        [HttpPost("DeleteFarmerSaleDetail")]
        public async Task<ActionResult> DeleteFarmerSaleDetails(int id, int userId, string accessToken)
        {
            var (status_code, status_message) = await _farmerSaleService.DeleteFarmerSaleDetails(id, userId, accessToken);
            if (status_code == "0")
            {
                return Ok(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message });
            }
        }
    }
}
