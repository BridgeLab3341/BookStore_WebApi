using BusinessLayer.Inteface;
using CommonLayer.Models;
using RepoLayer.Context.Models;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class CustomerDetailsBusiness:ICustomerDetailsBusiness
    {
        private readonly ICustomerDetailsRepo _customerDetailsRepo;
        public CustomerDetailsBusiness(ICustomerDetailsRepo customerDetailsRepo)
        {
            this._customerDetailsRepo = customerDetailsRepo;
        }
        public async Task<CustomerDetailsTable> AddCustomerDetails(CustomerDetailsModel model, long RegistrationId)
        {
            try
            {
                return await _customerDetailsRepo.AddCustomerDetails(model, RegistrationId);
            }
            catch(Exception)
            {
                throw new Exception("Customer Details Not Added UnSuccessful");
            }
        }
        public async Task<List<CustomerDetailsTable>> GetAllCustomerDetails()
        {
            try
            {
                return await _customerDetailsRepo.GetAllCustomerDetails();
            }
            catch(Exception)
            {
                throw new Exception("Customer Details Not Found UnSuccessful");
            }
        }
    }
}
