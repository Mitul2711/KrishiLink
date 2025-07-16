using KrishiLink.DTO.Farmer;
using KrishiLink.Models.Farmer;
using KrishiLink.Repository.Auth.Interface;
using KrishiLink.Repository.Farmer.Interface;
using KrishiLink.Services.Farmer.Interface;

namespace KrishiLink.Service.Farmer
{
    public class FarmerSaleService : IFarmerSaleService
    {
        private readonly IFarmerSaleRepository _farmerSaleRepository;

        public IUserRepository _UserRepository { get; }

        public FarmerSaleService(IFarmerSaleRepository farmerSaleRepository, IUserRepository userRepository)
        {
            _farmerSaleRepository = farmerSaleRepository;
            _UserRepository = userRepository;
        }

        public async Task<(string status_code, string status_message, IEnumerable<FarmerSale> Data)> GetAllFarmerSaleDetails(int userId, string access_token)
        {
            var isAccess = await _UserRepository.CheckAccess(userId, access_token);
            if (isAccess == null)
            {
                return ("0", "Session Expired!", null);
            }

            var result = await _farmerSaleRepository.GetAllFarmerSaleDetails(userId);
            if (result == null || !result.Any())
            {
                return ("0", "No farmer sales data found.", null);
            }

            return ("1", "Farmer sales data retrieved successfully.", result);
        }

        public async Task<(string status_code, string status_message, FarmerSale Data)> GetFarmerSaleDetailsById(int id, int userId, string access_token)
        {

            var isAccess = await _UserRepository.CheckAccess(userId, access_token);
            if (isAccess == null)
            {
                return ("0", "Session Expired!", null);
            }

            var result = await _farmerSaleRepository.GetFarmerSaleDetailsById(id, userId);
            if (result == null)
            {
                return ("0", $"No farmer sale found with ID = {id}.", null);
            }

            return ("1", "Farmer sale data retrieved successfully.", result);
        }

        public async Task<(string status_code, string status_message)> AddFarmerSaleDetails(FarmerSaleDTO farmerSaleDTO)
        {

            var isAccess = await _UserRepository.CheckAccess(farmerSaleDTO.UserId, farmerSaleDTO.AccessToken);
            if (isAccess == null)
            {
                return ("0", "Session Expired!");
            }
            if (farmerSaleDTO == null)
            {
                return ("0", "Data is required.");
            }

            try
            {
                await _farmerSaleRepository.AddFarmerSaleDetails(farmerSaleDTO);
                return ("1", "Data added successfully!");
            }
            catch (Exception ex)
            {
                return ("0", $"Failed to add data: {ex.Message}");
            }
        }

        public async Task<(string status_code, string status_message)> UpdateFarmerSaleDetails(FarmerSaleTokenDTO farmerSaleTokenDTO)
        {
            var isAccess = await _UserRepository.CheckAccess(farmerSaleTokenDTO.UserId, farmerSaleTokenDTO.AccessToken);
            if (isAccess == null)
            {
                return ("0", "Session Expired!");
            }

            if (farmerSaleTokenDTO == null)
            {
                return ("0", "Data is required.");
            }

            var data = await _farmerSaleRepository.GetFarmerSaleDetailsById(farmerSaleTokenDTO.FarmerId, farmerSaleTokenDTO.UserId);
            if (data == null)
            {
                return ("0", "Data Not Found");
            }

            try
            {
                await _farmerSaleRepository.UpdateFarmerSaleDetails(farmerSaleTokenDTO);
                return ("1", "Data updated successfully!");
            }
            catch (Exception ex)
            {
                return ("0", $"Failed to update data: {ex.Message}");
            }
        }

        public async Task<(string status_code, string status_message)> DeleteFarmerSaleDetails(int id, int userId, string accessToken)
        {

            var isAccess = await _UserRepository.CheckAccess(userId, accessToken);
            if (isAccess == null)
            {
                return ("0", "Session Expired!");

            }
            if (id == 0)
            {
                return ("0", "Id Is Not Exists!");
            }

            var data = await _farmerSaleRepository.GetFarmerSaleDetailsById(id, userId);
            if (data == null)
            {
                return ("0", "Data Not Found");
            }

            try
            {
                await _farmerSaleRepository.DeleteFarmerSaleDetails(id, userId);
                return ("1", "Data Deleted");
            }
            catch (Exception ex)
            {
                return ("0", $"Failed to Delete data: {ex.Message}");
            }

        }

    }
}
