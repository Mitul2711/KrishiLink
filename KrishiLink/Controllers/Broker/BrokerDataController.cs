using KrishiLink.DTO.Access_Token;
using KrishiLink.DTO.Broker;
using KrishiLink.Service.Broker.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KrishiLink.Controllers.Broker
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrokerDataController : ControllerBase
    {
        private readonly IBrokerDataService _brokerDataService;

        public BrokerDataController(IBrokerDataService brokerDataService)
        {
            _brokerDataService = brokerDataService;
        }

        [HttpPost("GetAllBrokerData")]
        public async Task<IActionResult> GetAllBrokerData(Access_TokenDTO access_TokenDTO)
        {
            var (status_code, status_message, Data) = await _brokerDataService.GetAllBrokerData(access_TokenDTO.UserId, access_TokenDTO.AccessToken);
            if (status_code == "0")
            {
                return Ok(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message, Data });
            }
        }

        [HttpPost("GetBrokerDataById")]
        public async Task<IActionResult> GetBrokerDataById(int id, int userId, string access_token)
        {
            var (status_code, status_message, Data) = await _brokerDataService.GetBrokerDataById(id, userId, access_token);
            if (status_code == "0")
            {
                return Ok(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message, Data });
            }
        }

        [HttpPost("AddBrokerData")]
        public async Task<ActionResult> AddBrokerData(BrokerDataDTO brokerDataDTO)
        {
            var (status_code, status_message) = await _brokerDataService.AddBrokerData(brokerDataDTO);
            if (status_code == "0")
            {
                return Ok(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message });
            }
        }

        [HttpPost("UpdateBrokerData")]
        public async Task<ActionResult> UpdateBrokerData(BrokerSaleTokenDTO brokerSaleTokenDTO)
        {
            var (status_code, status_message) = await _brokerDataService.UpdateBrokerData(brokerSaleTokenDTO);
            if (status_code == "0")
            {
                return Ok(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message });
            }
        }

        [HttpPost("DeleteBrokerData")]
        public async Task<ActionResult> DeleteBrokerData(int id, int userId, string access_token)
        {
            var (status_code, status_message) = await _brokerDataService.DeleteBrokerData(id, userId, access_token);
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
