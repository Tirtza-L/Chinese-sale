using Project.Models;

namespace Project.BLL
{
    public interface ISaleService
    {
        Task<Sale> AddSale(Sale sale);
        Task<Sale>DeleteSale (int idSale);
        Task<List<Sale>> GetAllSalesByStatusT();
        Task<List<Sale>> GetSaleByCustomer(int customerId);
        Task<List<Sale>> GetSaleByGift(int giftId);
        Task<Sale> GetSaleById(int saleId);
        Task<Sale> ChangeToStatusT(int saleId);
        Task<Customer> Random(int giftId);
       // Gift UpdateSaleCount(int idSale, int count);
    }
}
