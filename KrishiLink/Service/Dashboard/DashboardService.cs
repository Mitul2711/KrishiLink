using KrishiLink.DTO.Broker;
using KrishiLink.DTO.Dashboard;
using KrishiLink.Models.DashBoard;
using KrishiLink.Repository.Auth.Interface;
using KrishiLink.Repository.Dashboard.Interface;
using KrishiLink.Service.Dashboard.Interface;

namespace KrishiLink.Service.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IUserRepository _userRepository;

        public DashboardService(IDashboardRepository dashboardRepository, IUserRepository userRepository)
        {
            _dashboardRepository = dashboardRepository;
            _userRepository = userRepository;
        }

        public IUserRepository UserRepository { get; }

        public async Task<(string status_code, string status_message, DashBoard Data)> GetDashBoardInfo(DashBoardDTO dashboardDTO)
        {
            var isAccess = await _userRepository.CheckAccess(dashboardDTO.UserId, dashboardDTO.AccessToken);
            if (isAccess == null)
            {
                return ("0", "Session Expired!", null);
            }
            var dashboardData = await _dashboardRepository.GetDashBoardInfo(dashboardDTO);
            if(dashboardData == null)
            {
                return ("0", "Data Not Found", null);
            }
            return ("1", "Dashboard Data retrieved successfully.", dashboardData);
        }
    }
}
