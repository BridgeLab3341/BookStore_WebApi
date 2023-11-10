using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class CustomerDetailsModel
    {
        public string AddressType { get; set; }
        public string AreaBuilding { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
