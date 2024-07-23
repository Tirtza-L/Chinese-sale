using Azure;
using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Models.DTO;
using Microsoft.EntityFrameworkCore;


namespace Project.DAL
{
    public class CustomerDal:ICustomerDal
    {
        private readonly Context context;

        private readonly ILogger<CategoryDal> _logger;

        public CustomerDal(Context context,ILogger<CategoryDal> _logger)
        {
            this.context = context;
            this._logger = _logger;
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            try
            {
                await context.Customers.AddAsync(customer);
                await context.SaveChangesAsync();
                return customer;
            }
            catch (Exception)
            {
                _logger.LogInformation("customer not added");
                return null;
            }
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            try
            {
                return await context.Customers.Select(x => x).ToListAsync();
            }
            catch (Exception)
            {
                _logger.LogInformation("customers not found");
                return null;
            }
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            try
            {
                return await context.Customers.FirstAsync(x => x.Id==customerId);
            }
            catch (Exception)
            {
                _logger.LogInformation("customer not found");
                return null;
            }
        }

        public async Task<Customer> DeleteCustomer(int IdCustomer)
        {
            try
            {
                Customer customer = await context.Customers.FirstAsync(x => x.Id == IdCustomer);
                context.Customers.Remove(customer);
                await context.SaveChangesAsync();
                return customer;

            }
            catch (Exception)
            {
                _logger.LogInformation("customer not deleted");
                return null;
            }

        }
        public async Task<Customer> ChangeDetails(Customer customer)
        {
            try
            {
                context.Customers.Update(customer);
                await context.SaveChangesAsync();
                return customer;

            }
            catch (Exception)
            {
                _logger.LogInformation("customer details not changed");
                return null;
            }
        }
        public async Task<Customer> Login(string name, string password)
        {
            try
            {
                return await context.Customers.FirstAsync(x=>x.Name==name&&x.Password==password);
            }
            catch (Exception)
            {
                _logger.LogError("eroor login");
                return null;
            }
        }

    }
}
