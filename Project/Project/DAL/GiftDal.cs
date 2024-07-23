using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Models;
using Project.Models.DTO;

namespace Project.DAL
{
    public class GiftDal:IGiftDal
    {
        private readonly Context context;
        //private readonly ISaleDal saleDal;


        public GiftDal(Context context,ISaleDal saleDal)
        {
            this.context = context;
            //this.saleDal = saleDal;
        }
        public async Task<List<Gift>> GetAllGifts()
        {
            try
            {
                return await context.Gifts.Select(x => x).Include(g=>g.Customer).Include(g=>g.Category1).Where(x=>x.Price>0).ToListAsync();
            }
            catch (Exception)
            {
                throw /*Exception ex*/;
            }
        }

        public async Task<List<Gift>> GetAllNoGifts()
        {
            try
            {
                return await context.Gifts.Select(x => x).Include(g => g.Customer).Include(g => g.Category1).ToListAsync();
            }
            catch (Exception)
            {
                throw /*Exception ex*/;
            }
        }
        public async Task<Gift> AddGift(Gift gift)
        {
            try
            {
                await context.Gifts.AddAsync(gift);
                await context.SaveChangesAsync();
                return gift;
            }
            catch (Exception ex)
            {

                throw new Exception($"you have a problem with your input {ex.InnerException}");
            }

        }

        public async Task<Gift> DeleteGift(int IdGift)
        {
            try
            {
                Gift gift = await context.Gifts.FirstOrDefaultAsync(x => x.Id == IdGift);

                if (gift.Price!=-1)
                {
                    //List<Sale> s= await saleDal.GetSaleByGift(IdGift);
                    //if (s.Count()!=0)
                    //{
                    //    throw new Exception("there is a sale of this gift");
                    //}
                }
                context.Gifts.Remove(gift);
                await context.SaveChangesAsync();
                return gift;

            }
            catch (Exception ex)
            {
                throw /*Exception ex*/;

            }

        }
        public async Task<Gift> ChangeDetails(Gift gift)
        {
            try
            {
                context.Gifts.Update(gift);
                await context.SaveChangesAsync();
                return gift;

            }
            catch (Exception ex)
            {
                throw /*Exception ex*/;
            }
        }

        public async Task<Gift> ChangeStatusT(ChangeGiftStatusDto changeGiftStatusDto)
        {
            try
            {
                Gift g= await context.Gifts.FirstOrDefaultAsync(x => x.Id == changeGiftStatusDto.Id);
                g.Name = changeGiftStatusDto.Name;
                g.Description = changeGiftStatusDto.Description;
                g.Price = changeGiftStatusDto.Price;
                g.CategoryId = changeGiftStatusDto.CategoryId;
                context.Gifts.Update(g);
                await context.SaveChangesAsync();
                return g;

            }
            catch (Exception ex)
            {
                throw /*Exception ex*/;

            }
        }

        public async Task<List<Gift>> GetGiftsByDonor(int donorId)
        {

            try
            {
                return await context.Gifts.Select(x => x).Where(x => x.CustomerId == donorId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw /*Exception ex*/;

            }
        }
        public async Task AddWinnerToGift(int giftId)
        {
            try
            {
                Gift gift = await context.Gifts.FirstAsync(x => x.Id == giftId);
                gift.HasWinner = true;
                context.Gifts.Update(gift);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw /*Exception ex*/;

            }
        }
        public async Task<List<Gift>>RemoveAllWinners()
        {
            List<Gift>g=await context.Gifts.Select(x=>x).ToListAsync();
            foreach (Gift aaa in g)
            {
                aaa.HasWinner = false;
            }
            context.Gifts.UpdateRange(g);
            await context.SaveChangesAsync();
            return g;
        }
        public async Task<bool> HasWinnerToGift(int giftId)
        {
            try
            {
                Gift gift = await context.Gifts.FirstAsync(x => x.Id == giftId);
                return gift.HasWinner;

            }
            catch (Exception ex)
            {
                throw /*Exception ex*/;

            }
        }

    }
}
