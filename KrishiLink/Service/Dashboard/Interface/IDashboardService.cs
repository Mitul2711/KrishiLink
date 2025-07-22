using KrishiLink.DTO.Dashboard;
using KrishiLink.Models.DashBoard;

namespace KrishiLink.Service.Dashboard.Interface
{
    public interface IDashboardService
    {
        Task<(string status_code, string status_message, DashBoard Data)> GetDashBoardInfo(DashBoardDTO dashboardDTO);
    }
}
