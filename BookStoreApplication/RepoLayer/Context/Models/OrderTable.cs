using System;
using System.Collections.Generic;

namespace RepoLayer.Context.Models
{
    public partial class OrderTable
    {
        public long OrderId { get; set; }
        public long? CustomerDetailId { get; set; }
        public DateTime? OrderTime { get; set; }
        public long? ProductId { get; set; }

        public virtual CustomerDetailsTable CustomerDetail { get; set; }
        public virtual ProductTable Product { get; set; }
    }
}
