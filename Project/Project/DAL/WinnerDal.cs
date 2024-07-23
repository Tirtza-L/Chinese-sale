using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.DAL
{
    public class WinnerDal:IWinnerDal
    {
        private readonly Context context;
        public WinnerDal(Context context/*,IGiftDal giftDal, ICustomerDal customerDal*/)
        {
            this.context = context;
        }
        public async Task<List<Winner>> GetAllWinners()
        {
            try
            {
                return await context.Winners.Select(x => x).ToListAsync();
            }
            catch (Exception)
            {
                throw /*Exception ex*/;
            }
        }
        public async Task<Winner> AddWinner(Winner winner)
        {
            try
            {
                await context.Winners.AddAsync(winner);
                await context.SaveChangesAsync();
                return winner;
            }
            catch (Exception ex)
            {

                throw new Exception($"you have a problem with your input {ex.InnerException}");
            }
        }

        public async Task<List<Winner>> DeleteAll()
        {
            try
            {
                List<Winner> w = await context.Winners.Select(x => x).ToListAsync();
                context.Winners.RemoveRange(w);
                await context.SaveChangesAsync();
                return w;
            }
            catch (Exception ex)
            {

                throw new Exception($"you have a problem with your input {ex.InnerException}");
            }
        }
        public async Task<Winner> GetWinnerByGift(int giftId)
        {
            try
            {
                Winner w = await context.Winners.FirstAsync(x => x.GiftId == giftId);
                return w;
            }
            catch (Exception ex)
            {
                throw new Exception($"you have a problem with your input {ex.InnerException}");
            }
        }

    }
}
