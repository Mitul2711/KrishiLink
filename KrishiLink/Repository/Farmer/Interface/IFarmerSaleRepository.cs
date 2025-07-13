using KrishiLink.DTO.Farmer;
using KrishiLink.Models.Farmer;

namespace KrishiLink.Repository.Farmer.Interface
{
    public interface IFarmerSaleRepository
    {
        public Task<IEnumerable<FarmerSale>> GetAllFarmerSaleDetails(int userId);
        public Task<FarmerSale> GetFarmerSaleDetailsById(int id, int userId);

        public Task AddFarmerSaleDetails(FarmerSaleDTO farmerSaleDTO);
        public Task UpdateFarmerSaleDetails(FarmerSaleTokenDTO farmerSaleTokenDTO);

        public Task DeleteFarmerSaleDetails(int id, int userId);
    }
}
