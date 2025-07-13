using KrishiLink.DTO.Farmer;
using KrishiLink.Models.Farmer;

namespace KrishiLink.Services.Farmer.Interface
{
    public interface IFarmerSaleService
    {
        Task<(bool IsSuccess, string Message, IEnumerable<FarmerSale> Data)> GetAllFarmerSaleDetails();
        Task<(bool IsSuccess, string Message, FarmerSale Data)> GetFarmerSaleDetailsById(int id);

        Task<(bool IsSuccess, string Message)> AddFarmerSaleDetails(FarmerSaleDTO farmerSaleDTO);
        Task<(bool IsSuccess, string Message)> UpdateFarmerSaleDetails(FarmerSale farmerSale);
        Task<(bool IsSuccess, string Message)> DeleteFarmerSaleDetails(int id);
    }
}
