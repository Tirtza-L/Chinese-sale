using Project.DAL;
using Project.Models;

namespace Project.BLL
{
    public class WinnerService:IWinnerService
    {
        private readonly IWinnerDal IWennerD;
        private readonly ICustomerDal customerDal;
        private readonly IGiftDal giftDal;
        public WinnerService(IWinnerDal winnerDal,ICustomerDal customerDal,IGiftDal giftDal) 
        { 
            this.IWennerD= winnerDal;
            this.customerDal= customerDal;
            this.giftDal= giftDal;
        }  
        public Task<List<Winner>> GetAllWinners()
        {
            return IWennerD.GetAllWinners();
        }
        public async Task<Winner> AddWinner(Winner winner)
        {
            Winner w=await IWennerD.AddWinner(winner);
            await giftDal.AddWinnerToGift(winner.GiftId);
            return w;

        }
        public async Task<List<Winner>> DeleteAll()
        {
            List<Winner>w=await IWennerD.DeleteAll();
            await giftDal.RemoveAllWinners();
            return w;
        }
        public async Task<Customer> GetWinnerByGift(int giftId)
        {
            Winner w=await IWennerD.GetWinnerByGift(giftId);
            return await customerDal.GetCustomerById(w.CustomerId);
        }

    }
}
