using KrishiLink.DTO.Dashboard;
using KrishiLink.Models.DashBoard;

namespace KrishiLink.Repository.Dashboard.Interface
{
    public interface IDashboardRepository
    {
        public Task<DashBoard> GetDashBoardInfo(DashBoardDTO dashboardDTO); 
    }
}
