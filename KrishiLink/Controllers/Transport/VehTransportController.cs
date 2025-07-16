using KrishiLink.DTO.Access_Token;
using KrishiLink.DTO.Transport;
using KrishiLink.Service.Transport.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KrishiLink.Controllers.Transport
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehTransportController : ControllerBase
    {
        private readonly IVehTransportService _vehTransportService;

        public VehTransportController(IVehTransportService vehTransportService)
        {
            _vehTransportService = vehTransportService;
        }

        [HttpPost("GetAllVehTransportDetail")]
        public async Task<ActionResult> GetAllVehTransportDetail(Access_TokenDTO access_TokenDTO)
        {
            var (status_code, status_message, data) = await _vehTransportService.GetAllVehTransportDetail(access_TokenDTO.UserId, access_TokenDTO.AccessToken);
            if (status_code == "0")
            {
                return Ok(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message, data });
            }
        }

        [HttpPost("GetVehTransportDetailById")]
        public async Task<ActionResult> GetVehTransportDetailById(int id, int userId, string access_token)
        {
            var (status_code, status_message, data) = await _vehTransportService.GetVehTransportDetailById(id, userId, access_token);
            if (status_code == "0")
            {
                return Ok(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message, data });
            }
        }

        [HttpPost("AddVehTransportDetail")]
        public async Task<ActionResult> AddVehTransportDetail(VehicleTransportDataDTO vehicleTransportDataDTO)
        {
            var (status_code, status_message) = await _vehTransportService.AddVehTransportDetail(vehicleTransportDataDTO);
            if (status_code == "0")
            {
                return Ok(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message });
            }
        }

        [HttpPost("UpdateVehTransportDetail")]
        public async Task<ActionResult> UpdateVehTransportDetail(VehicalTransportDataTokenDTO vehicalTransportDataTokenDTO)
        {
            var (status_code, status_message) = await _vehTransportService.UpdateVehTransportDetail(vehicalTransportDataTokenDTO);
            if (status_code == "0")
            {
                return Ok(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message });
            }
        }

        [HttpPost("DeleteVehTransportDetail")]
        public async Task<ActionResult> DeleteVehTransportDetail(int id, int userId, string access_token)
        {
            var (status_code, status_message) = await _vehTransportService.DeleteVehTransportDetail(id, userId, access_token);
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
