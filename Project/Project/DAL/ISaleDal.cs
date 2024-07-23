using Project.Models;

namespace Project.DAL
{
    public interface ISaleDal
    {
        Task<Sale> AddSale(Sale sale);
        Task<Sale> DeleteSale(int idSale);
        Task<List<Sale>> GetAllSalesByStatusT();
        Task<List<Sale>> GetSaleByCustomer(int customerId);
        Task<Sale> GetSaleById(int saleId);
        Task<Sale> ChangeToStatusT(int saleId);
        Task<List<Sale>> GetSaleByGift(int giftId);
    }
}
