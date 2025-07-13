using KrishiLink.DTO.Broker;
using KrishiLink.DTO.Farmer;
using KrishiLink.Models.Broker;
using KrishiLink.Models.Farmer;
using KrishiLink.Service.Broker.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KrishiLink.Controllers.Broker
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BrokerDataController : ControllerBase
    {
        private readonly IBrokerDataService _brokerDataService;

        public BrokerDataController(IBrokerDataService brokerDataService)
        {
            _brokerDataService = brokerDataService;
        }

        [HttpGet("GetAllBrokerData")]
        public async Task<IActionResult> GetAllBrokerData()
        {
            var (IsSuccess, Message, Data) = await _brokerDataService.GetAllBrokerData();
            if (!IsSuccess)
            {
                return NotFound(new { IsSuccess, Message });
            }
            else
            {
                return Ok(new { IsSuccess, Message, Data });
            }
        }

        [HttpGet("GetBrokerDataById")]
        public async Task<IActionResult> GetBrokerDataById(int id)
        {
            var (IsSuccess, Message, Data) = await _brokerDataService.GetBrokerDataById(id);
            if (!IsSuccess)
            {
                return NotFound(new { IsSuccess, Message });
            }
            else
            {
                return Ok(new { IsSuccess, Message, Data });
            }
        }

        [HttpPost("AddBrokerData")]
        public async Task<ActionResult> AddBrokerData(BrokerDataDTO brokerDataDTO)
        {
            var (IsSuccess, Message) = await _brokerDataService.AddBrokerData(brokerDataDTO);
            if (!IsSuccess)
            {
                return NotFound(new { IsSuccess, Message });
            }
            else
            {
                return Ok(new { IsSuccess, Message });
            }
        }

        [HttpPut("UpdateBrokerData")]
        public async Task<ActionResult> UpdateBrokerData(BrokerData brokerData)
        {
            var (IsSuccess, Message) = await _brokerDataService.UpdateBrokerData(brokerData);
            if (!IsSuccess)
            {
                return NotFound(new { IsSuccess, Message });
            }
            else
            {
                return Ok(new { IsSuccess, Message });
            }
        }

        [HttpDelete("DeleteBrokerData")]
        public async Task<ActionResult> DeleteBrokerData(int id)
        {
            var (IsSuccess, Message) = await _brokerDataService.DeleteBrokerData(id);
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
