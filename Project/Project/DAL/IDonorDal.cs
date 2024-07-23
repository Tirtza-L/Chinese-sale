using Project.Models;
using Project.Models.DTO;

namespace Project.DAL
{
    public interface IDonorDal
    {
        Task<List<Customer>> GetAllDonors();
        public Task<Customer> DeleteDonor(int IdDonor);
        public Task<Customer> ChangeDetails( DonorDto donorDto);
        public Task<Customer> ChangeToDonor(int IdDonor);
        public Task<Customer> GetDonorByGift(int GiftId);
    }
}
