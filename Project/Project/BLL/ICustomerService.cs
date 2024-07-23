using Project.DAL;
using Project.Models;
using Project.Models.DTO;
using System.Numerics;

namespace Project.BLL
{
    public interface ICustomerService
    {
       
        public Task<Customer> AddCustomer(Customer customer);
        public Task<List<Customer>> GetAllCustomers();
        public Task<Customer> DeleteCustomer(int IdCustomer);
        public Task<Customer> ChangeDetails(int IdCustomer, Customer customer);
        public Task<Customer> GetCustomerById(int customerId);
        public Task<Customer> Login(string name, string password);
    }
}
