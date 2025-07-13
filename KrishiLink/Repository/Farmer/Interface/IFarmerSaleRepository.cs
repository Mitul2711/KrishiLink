using KrishiLink.Models.Farmer;

namespace KrishiLink.Repository.Farmer.Interface
{
    public interface IFarmerSaleRepository
    {
        public Task<IEnumerable<FarmerSale>> GetAllFarmerSaleDetails();
        public Task<FarmerSale> GetFarmerSaleDetailsById(int id);

        public Task AddFarmerSaleDetails(FarmerSale farmerSale);
        public Task UpdateFarmerSaleDetails(FarmerSale farmerSale);

        public Task DeleteFarmerSaleDetails(int id);
    }
}
