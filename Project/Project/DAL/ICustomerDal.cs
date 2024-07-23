using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Models.DTO;

namespace Project.DAL
{
    public interface ICustomerDal
    {

        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> DeleteCustomer(int idCustomer);
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int customerId);
        Task<Customer> ChangeDetails(Customer customer);
        Task<Customer> Login(string name, string password);
    }
}
