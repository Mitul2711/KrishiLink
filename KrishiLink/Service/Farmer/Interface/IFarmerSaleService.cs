using KrishiLink.DTO.Farmer;
using KrishiLink.Models.Farmer;

namespace KrishiLink.Services.Farmer.Interface
{
    public interface IFarmerSaleService
    {
        Task<(string status_code, string status_message, IEnumerable<FarmerSale> Data)> GetAllFarmerSaleDetails(int userId, string access_token);
        Task<(string status_code, string status_message, FarmerSale Data)> GetFarmerSaleDetailsById(int id, int userId, string access_token);

        Task<(string status_code, string status_message)> AddFarmerSaleDetails(FarmerSaleDTO farmerSaleDTO);
        Task<(string status_code, string status_message)> UpdateFarmerSaleDetails(FarmerSaleTokenDTO farmerSaleTokenDTO);
        Task<(string status_code, string status_message)> DeleteFarmerSaleDetails(int id, int userId, string accessToken);
    }
}
