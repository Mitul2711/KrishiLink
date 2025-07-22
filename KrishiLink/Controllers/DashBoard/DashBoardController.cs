using KrishiLink.DTO.Dashboard;
using KrishiLink.Service.Dashboard.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KrishiLink.Controllers.DashBoard
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashBoardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpPost("GetDashBoardInfo")]
        public async Task<ActionResult> GetDashBoardInfo(DashBoardDTO dashBoardDTO)
        {
            var (status_code, status_message, Data) = await _dashboardService.GetDashBoardInfo(dashBoardDTO);
            if (status_code == "0")
            {
                return Ok(new { status_code, status_message });
            }
            else
            {
                return Ok(new { status_code, status_message, Data });
            }
        }

    }
}
