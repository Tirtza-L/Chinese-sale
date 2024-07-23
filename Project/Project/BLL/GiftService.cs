using Microsoft.AspNetCore.Mvc;
using Project.DAL;
using Project.Models;
using Project.Models.DTO;

namespace Project.BLL
{
    public class GiftService: IGiftService
    {
        //add,update,delete(if no one buy...) gift
        //change category
        private readonly IGiftDal GiftDal;
        private readonly IDonorDal DonorDal;
        public GiftService(IGiftDal IGiftDal,IDonorDal IDonorDal)
        {
            this.GiftDal = IGiftDal;
            this.DonorDal = IDonorDal;
        }

        public async Task<List<Gift>> GetAllGifts()
        {
            return await GiftDal.GetAllGifts();
        }

        public async Task<List<Gift>> GetAllNoGifts()
        {
            return await GiftDal.GetAllNoGifts();
        }
        public async Task<Gift> AddGift(Gift gift)
        {
           
            return await GiftDal.AddGift(gift);
        }

        public async Task<Gift> DeleteGift(int IdGift)
        {
            return await GiftDal.DeleteGift(IdGift);
        }

        public async Task<List<Gift>> GetGiftsByDonor(int DonorId)
        {
            return await GiftDal.GetGiftsByDonor(DonorId);
        }


        public async Task<Gift> ChangeDetails(int IdGift, Gift gift)
        {
            gift.Id = IdGift;

            return await GiftDal.ChangeDetails(gift);
        }

        public async Task<Gift> ChangeStatusT(ChangeGiftStatusDto changeGiftStatusDto)
        {
            Gift g= await GiftDal.ChangeStatusT(changeGiftStatusDto);
            if (g !=null)
                try
                {
                   await DonorDal.ChangeToDonor(g.CustomerId);
                }
                catch
                {
                    throw new Exception("hjfdiok");
                }
            return g;
        }
    }
}
