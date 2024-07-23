using Project.Models;

namespace Project.DAL
{
    public interface IWinnerDal
    {
        Task<List<Winner>> GetAllWinners();
        Task<Winner> AddWinner(Winner winner);
        Task<List<Winner>> DeleteAll();
        Task<Winner> GetWinnerByGift(int giftId);

    }
}
