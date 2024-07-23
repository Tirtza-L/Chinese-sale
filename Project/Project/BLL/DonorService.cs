using Project.DAL;
using Project.Models.DTO;
using Project.Models;

namespace Project.BLL
{
    public class DonorService:IDonorService
    {
        //add,update... donor
        private readonly IDonorDal DonorDal;

        public DonorService(IDonorDal IDonorDal)
        {
            this.DonorDal = IDonorDal;
        }

        public async Task<List<Customer>> GetAllDonors()
        {
            return await DonorDal.GetAllDonors();
        }

        public async Task<Customer> DeleteDonor(int IdDonor)
        {
            return await DonorDal.DeleteDonor(IdDonor);
        }

        public async Task<Customer> ChangeDetails(DonorDto donorDto)
        {
            return await DonorDal.ChangeDetails(donorDto);
        }
        public async Task<Customer> ChangeToDonor(int IdDonor)
        {
            return await DonorDal.ChangeToDonor(IdDonor);
        }
        public async Task<Customer> GetDonorByGift(int GiftId)
        {
            return await DonorDal.GetDonorByGift(GiftId);
        }
    }
}
