using CommonLayer.Models;
using RepoLayer.Context.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface ICustomerDetailsRepo
    {
        public Task<CustomerDetailsTable> AddCustomerDetails(CustomerDetailsModel model, long RegistrationId);
        public Task<List<CustomerDetailsTable>> GetAllCustomerDetails();
    }
}
