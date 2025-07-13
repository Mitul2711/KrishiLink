using KrishiLink.DTO.Transport;
using KrishiLink.Service.Transport.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KrishiLink.Controllers.Transport
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VehTransportController : ControllerBase
    {
        private readonly IVehTransportService _vehTransportService;

        public VehTransportController(IVehTransportService vehTransportService)
        {
            _vehTransportService = vehTransportService;
        }

        [HttpGet("GetAllVehTransportDetail")]
        public async Task<ActionResult> GetAllVehTransportDetail()
        {
            var (IsSuccess, Message, data) = await _vehTransportService.GetAllVehTransportDetail();
            if (!IsSuccess)
            {
                return NotFound(new { IsSuccess, Message });
            }
            else
            {
                return Ok(new { IsSuccess, Message, data });
            }
        }

        [HttpGet("GetVehTransportDetailById")]
        public async Task<ActionResult> GetVehTransportDetailById(int id)
        {
            var (IsSuccess, Message, data) = await _vehTransportService.GetVehTransportDetailById(id);
            if (!IsSuccess)
            {
                return NotFound(new { IsSuccess, Message });
            }
            else
            {
                return Ok(new { IsSuccess, Message, data });
            }
        }

        [HttpPost("AddVehTransportDetail")]
        public async Task<ActionResult> AddVehTransportDetail(VehicleTransportDataDTO vehicleTransportDataDTO)
        {
            var (IsSuccess, Message) = await _vehTransportService.AddVehTransportDetail(vehicleTransportDataDTO);
            if (!IsSuccess)
            {
                return NotFound(new { IsSuccess, Message });
            }
            else
            {
                return Ok(new { IsSuccess, Message });
            }
        }

        [HttpPut("UpdateVehTransportDetail")]
        public async Task<ActionResult> UpdateVehTransportDetail(GetVehTransportDTO getVehTransportDTO)
        {
            var (IsSuccess, Message) = await _vehTransportService.UpdateVehTransportDetail(getVehTransportDTO);
            if (!IsSuccess)
            {
                return NotFound(new { IsSuccess, Message });
            }
            else
            {
                return Ok(new { IsSuccess, Message });
            }
        }

        [HttpDelete("DeleteVehTransportDetail")]
        public async Task<ActionResult> DeleteVehTransportDetail(int id)
        {
            var (IsSuccess, Message) = await _vehTransportService.DeleteVehTransportDetail(id);
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
