using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Project.DAL;
using Project.Models;
using Project.Models.DTO;
using System.Numerics;

namespace Project.BLL
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDal customerDal;

        public CustomerService(ICustomerDal customerDal)
        {
            this.customerDal = customerDal;
        }
       
        public async Task<Customer> AddCustomer(Customer customer)
        {
            //name
            if (customer.Name == "")
            {
                throw new InvalidOperationException("Name must be with at least 1 char");
            }
            //password
            if (customer.Password == "")
            {
                throw new InvalidOperationException("Password must be with at least 1 char");
            }
           //phone
            if (! int.TryParse(customer.Phone, out _))
            {
                throw new InvalidOperationException("Phone must contain only number");
            }
            if (customer.Phone.Length!=10)
            {
                throw new InvalidOperationException("Phone must contain 10 number exactly");
            }
            if (!customer.Phone.StartsWith("05"))
            {
                throw new InvalidOperationException("Phone invalid");
            }
            //Email
            if (customer.Email.Length<5|| customer.Email.IndexOf("@")<1|| customer.Email.LastIndexOf(".") > customer.Email.Length-2|| customer.Email.LastIndexOf("@")!= customer.Email.IndexOf("@"))
            {
                throw new InvalidOperationException("email invalid");
            }

            return await customerDal.AddCustomer(customer);
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await customerDal.GetAllCustomers();
        }
        public async Task<Customer> DeleteCustomer(int IdCustomer)
        {
            return await customerDal.DeleteCustomer(IdCustomer);
        }
        public async Task<Customer> ChangeDetails(int IdCustomer, Customer customer)
        {
            customer.Id = IdCustomer;

            return await customerDal.ChangeDetails( customer);
        }

        public async Task<Customer> Login(string name, string password)
        {
            return await customerDal.Login(name, password);
        }
        public async Task<Customer> GetCustomerById(int customerId)
        {
            return await customerDal.GetCustomerById(customerId);
        }
    }
}
