using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Context.Models;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class CustomerDetailsRepo : ICustomerDetailsRepo
    {
        private readonly IConfiguration _configuration;
        private readonly dbBooksContext _dbBooksContext;
        public CustomerDetailsRepo(IConfiguration configuration,dbBooksContext dbBooksContext)
        {
            this._configuration = configuration;
            this._dbBooksContext = dbBooksContext;
        }
        public async Task<CustomerDetailsTable> AddCustomerDetails(CustomerDetailsModel model,long RegistrationId)
        {
            try
            {
                CustomerDetailsTable customer= new CustomerDetailsTable();
                customer.AddressType = model.AddressType;
                customer.AreaBuilding= model.AreaBuilding;
                customer.City = model.City;
                customer.State=model.State;
                customer.PinCode=model.PinCode;
                customer.PhoneNumber=model.PhoneNumber;
                customer.RegisterId=RegistrationId;
                await _dbBooksContext.AddAsync(customer);
                await _dbBooksContext.SaveChangesAsync();
                return customer;

            }
            catch(Exception)
            {
                throw new Exception ("Customer Details Not Added UnSuccessful");
            }
        }
        public async Task<List<CustomerDetailsTable>> GetAllCustomerDetails()
        {
            try
            {
                List<CustomerDetailsTable> list= new List<CustomerDetailsTable>();
                list=await _dbBooksContext.CustomerDetailsTable.ToListAsync();
                if(list !=null)
                {
                    return list;
                }
                return null;
            }
            catch(Exception)
            {
                throw new Exception("Customer Details Not Found UnSuccessful");
            }
        }
    }
}
