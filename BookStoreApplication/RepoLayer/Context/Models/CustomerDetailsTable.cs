using System;
using System.Collections.Generic;

namespace RepoLayer.Context.Models
{
    public partial class CustomerDetailsTable
    {
        public CustomerDetailsTable()
        {
            OrderTable = new HashSet<OrderTable>();
        }

        public long CustomerDetailId { get; set; }
        public string AddressType { get; set; }
        public string AreaBuilding { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string PhoneNumber { get; set; }
        public long? RegisterId { get; set; }

        public virtual RegistrationTable Register { get; set; }
        public virtual ICollection<OrderTable> OrderTable { get; set; }
    }
}
