using Project.Models;
using Project.Models.DTO;

namespace Project.BLL
{
    public interface IGiftService
    {
        public Task<List<Gift>> GetAllGifts();
        public Task<List<Gift>> GetAllNoGifts();
        public Task<Gift> AddGift(Gift gift);
        public Task<Gift> DeleteGift(int IdGift);
        public Task<Gift> ChangeDetails(int IdGift, Gift gift);
        public Task<Gift> ChangeStatusT(ChangeGiftStatusDto changeGiftStatusDto);
        Task<List<Gift>> GetGiftsByDonor(int donorId);
    }
}
