using Project.Models;
using Project.Models.DTO;

namespace Project.BLL
{
    public interface IDonorService
    {
        public Task<Customer> DeleteDonor(int IdDonor);
        public Task<List<Customer>> GetAllDonors();
        public Task<Customer> ChangeDetails( DonorDto donorDto);
        public Task<Customer> ChangeToDonor(int IdDonor);
        Task<Customer> GetDonorByGift(int donorId);

    }
}
