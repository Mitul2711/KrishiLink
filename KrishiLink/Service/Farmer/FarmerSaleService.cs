using KrishiLink.DTO.Farmer;
using KrishiLink.Models.Farmer;
using KrishiLink.Repository.Farmer.Interface;
using KrishiLink.Services.Farmer.Interface;

namespace KrishiLink.Service.Farmer
{
    public class FarmerSaleService : IFarmerSaleService
    {
        private readonly IFarmerSaleRepository _farmerSaleRepository;

        public FarmerSaleService(IFarmerSaleRepository farmerSaleRepository)
        {
            _farmerSaleRepository = farmerSaleRepository;
        }

        public async Task<(bool IsSuccess, string Message, IEnumerable<FarmerSale> Data)> GetAllFarmerSaleDetails()
        {
            var result = await _farmerSaleRepository.GetAllFarmerSaleDetails();
            if (result == null || !result.Any())
            {
                return (false, "No farmer sales data found.", null);
            }

            return (true, "Farmer sales data retrieved successfully.", result);
        }

        public async Task<(bool IsSuccess, string Message, FarmerSale Data)> GetFarmerSaleDetailsById(int id)
        {
            var result = await _farmerSaleRepository.GetFarmerSaleDetailsById(id);
            if (result == null)
            {
                return (false, $"No farmer sale found with ID = {id}.", null);
            }

            return (true, "Farmer sale data retrieved successfully.", result);
        }

        public async Task<(bool IsSuccess, string Message)> AddFarmerSaleDetails(FarmerSaleDTO farmerSaleDTO)
        {
            if (farmerSaleDTO == null)
            {
                return (false, "Data is required.");
            }

            try
            {
                var farmerDetail = new FarmerSale
                {
                    Farmer_name = farmerSaleDTO.Farmer_name,
                    Mobile = farmerSaleDTO.Mobile,
                    Village = farmerSaleDTO.Village,
                    Crop_Name = farmerSaleDTO.Crop_Name,
                    Crop_Type = farmerSaleDTO.Crop_Type,
                    Weight = farmerSaleDTO.Weight,
                    Price = farmerSaleDTO.Price
                };

                await _farmerSaleRepository.AddFarmerSaleDetails(farmerDetail);
                return (true, "Data added successfully!");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to add data: {ex.Message}");
            }
        }

        public async Task<(bool IsSuccess, string Message)> UpdateFarmerSaleDetails(FarmerSale farmerSale)
        {
            if (farmerSale == null)
            {
                return (false, "Data is required.");
            }

            var data = await _farmerSaleRepository.GetFarmerSaleDetailsById(farmerSale.FarmerId);
            if (data == null)
            {
                return (false, "Data Not Found");
            }

            try
            {
                await _farmerSaleRepository.UpdateFarmerSaleDetails(farmerSale);
                return (true, "Data updated successfully!");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update data: {ex.Message}");
            }
        }

        public async Task<(bool IsSuccess, string Message)> DeleteFarmerSaleDetails(int id)
        {
            if(id == 0)
            {
                return (false, "Id Is Not Exists!");
            }

            var data = await _farmerSaleRepository.GetFarmerSaleDetailsById(id);
            if (data == null)
            {
                return (false, "Data Not Found");
            }

            try
            {
                await _farmerSaleRepository.DeleteFarmerSaleDetails(id);
                return (true, "Data Deleted");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to Delete data: {ex.Message}");
            }
            
        }

    }
}
