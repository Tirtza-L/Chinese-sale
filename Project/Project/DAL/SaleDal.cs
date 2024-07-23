using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.DAL
{
    public class SaleDal : ISaleDal
    {
        private readonly Context context;
        //private readonly IGiftDal giftDal;
        public SaleDal(Context context/*, IGiftDal giftDal*/)
        {
            this.context = context;
            //this.giftDal = giftDal;
        }
        public async Task<Sale> AddSale(Sale sale)
        {
            try
            {
                //bool cantBuy=await giftDal.HasWinnerToGift(sale.GiftId);
                //if (cantBuy) { throw new Exception("the loto was"); }
                Sale s=await context.Sales.FirstOrDefaultAsync(sa=>sa.GiftId==sale.GiftId&&sa.Status==false&&sa.CustomerId==sale.CustomerId);
                if (s!=null)
                {
                    s.Count += 1;
                    context.Sales.Update(s);
                    await context.SaveChangesAsync();
                    return s;
                }
                else
                {
                    await context.Sales.AddAsync(sale);
                    await context.SaveChangesAsync();
                    return sale;
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Sale> DeleteSale(int idSale)
        {
            try
            {
                Sale sale = await context.Sales.FirstOrDefaultAsync(x => x.Id == idSale);
                if (sale!=null)
                {
                    context.Sales.Remove(sale);
                    await context.SaveChangesAsync();                   
                }
                return sale;

            }
            catch (Exception ex)
            {
                throw /*Exception ex*/;

            }
        }
        public async Task<List<Sale>> GetAllSalesByStatusT()
        {
            try
            {
                return await context.Sales.Select(x => x).Where(x=>x.Status==true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw /*Exception ex*/;

            }
        }
        public async Task<List<Sale>> GetSaleByCustomer(int customerId)
        {
            try
            {
                return await context.Sales.Select(x => x).Where(x => x.CustomerId == customerId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw /*Exception ex*/;

            }
        }
        public async Task<List<Sale>> GetSaleByGift(int giftId)
        {
            try
            {
                return await context.Sales.Select(x => x).Include(x=>x.Customer).Where(x => x.GiftId == giftId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw /*Exception ex*/;

            }
        }
        public async Task<Sale> GetSaleById(int saleId)
        {
            try
            {
                return await context.Sales.FirstOrDefaultAsync(x => x.Id == saleId);
            }
            catch (Exception ex)
            {
                throw /*Exception ex*/;

            }
        }
        public async Task<Sale> ChangeToStatusT(int saleId)
        {
            try
            {
                Sale s= await context.Sales.FirstOrDefaultAsync(x => x.Id == saleId);
               
                s.Status=true;
                context.Sales.Update(s);
                await context.SaveChangesAsync();
                return s;
            }
            catch (Exception ex)
            {
                throw /*Exception ex*/;

            }
        }

    }
}
