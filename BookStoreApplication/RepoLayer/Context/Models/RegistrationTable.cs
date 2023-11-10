using System;
using System.Collections.Generic;

namespace RepoLayer.Context.Models
{
    public partial class RegistrationTable
    {
        public RegistrationTable()
        {
            CartTable = new HashSet<CartTable>();
            CustomerDetailsTable = new HashSet<CustomerDetailsTable>();
            OrderTable = new HashSet<OrderTable>();
            ProductTable = new HashSet<ProductTable>();
        }

        public long RegisterId { get; set; }
        public string TypeofRegister { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<CartTable> CartTable { get; set; }
        public virtual ICollection<CustomerDetailsTable> CustomerDetailsTable { get; set; }
        public virtual ICollection<OrderTable> OrderTable { get; set; }
        public virtual ICollection<ProductTable> ProductTable { get; set; }
    }
}
