using Project.Models.DTO;
using Project.Models;

namespace Project.BLL
{
    public interface IWinnerService
    {
        public Task<List<Winner>> GetAllWinners();
        public Task<Winner> AddWinner(Winner winner);
        public Task<List<Winner>> DeleteAll();
        public Task<Customer> GetWinnerByGift(int giftId);
    }
}
