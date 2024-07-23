using Project.Models;
using Project.Models.DTO;

namespace Project.DAL
{
    public interface IGiftDal
    {
        Task<List<Gift>> GetAllGifts();
        Task<List<Gift>> GetAllNoGifts();
        Task<Gift> AddGift(Gift gift);
        Task<Gift> DeleteGift(int idGift);
        Task<Gift> ChangeDetails(Gift gift);
        Task<Gift> ChangeStatusT(ChangeGiftStatusDto changeGiftStatusDto);
        Task<List<Gift>> GetGiftsByDonor(int donorId);
        Task AddWinnerToGift(int giftId);
        Task<bool> HasWinnerToGift(int giftId);
        Task<List<Gift>> RemoveAllWinners();

    }
}
