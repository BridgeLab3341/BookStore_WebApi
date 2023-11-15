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
        //Summary 
        //CustomerDetailsRepo class contains Add CustomerDetails by Customer in the bookstoreApplication.
        //Constructor is Implemented with parameters and Dependencies are added from Configuration and DbBookContext file.

        private readonly IConfiguration _configuration;
        private readonly dbBooksContext _dbBooksContext;
        public CustomerDetailsRepo(IConfiguration configuration,dbBooksContext dbBooksContext)
        {
            this._configuration = configuration;
            this._dbBooksContext = dbBooksContext;
        }
        //Summary
        //AddCustomerDetails method is Implemented to add customer details into the customer details table.
        //Adding the customer details by RegistrationId by customer only.
        public async Task<CustomerDetailsTable> AddCustomerDetails(CustomerDetailsModel model,long RegistrationId)
        {
            try
            {
                CustomerDetailsTable customer= new CustomerDetailsTable(); //Creating the Instance of the CustomerDetailsTable to add data.
                customer.AddressType = model.AddressType;
                customer.AreaBuilding= model.AreaBuilding;
                customer.City = model.City;
                customer.State=model.State;
                customer.PinCode=model.PinCode;
                customer.PhoneNumber=model.PhoneNumber;
                customer.RegisterId=RegistrationId;
                await _dbBooksContext.AddAsync(customer);  //Adding into the dbcontext.
                await _dbBooksContext.SaveChangesAsync(); //save the added details in the dbcontext database.
                return customer;

            }
            catch(Exception)
            {
                throw new Exception ("Customer Details Not Added UnSuccessful");
            }
        }
        //Summary
        //Fetch or Get All Customer Details from the Table.  
        public async Task<List<CustomerDetailsTable>> GetAllCustomerDetails()
        {
            try
            {
                List<CustomerDetailsTable> list= new List<CustomerDetailsTable>();  //List the CustomerDetailsTable by creating instance.
                list=await _dbBooksContext.CustomerDetailsTable.ToListAsync();  
                if(list !=null)
                {
                    return list;  //Print the details of customerDetails 
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
