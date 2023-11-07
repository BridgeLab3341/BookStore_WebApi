using System;
using System.Collections.Generic;

namespace RepoLayer.Context.Models
{
    public partial class ProductTable
    {
        public ProductTable()
        {
            CartTable = new HashSet<CartTable>();
            OrderTable = new HashSet<OrderTable>();
        }

        public long ProductId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public string Image { get; set; }
        public string Descrption { get; set; }
        public int? Quantity { get; set; }
        public string Status { get; set; }
        public decimal? Discountprice { get; set; }
        public decimal? Price { get; set; }
        public long? RegisterId { get; set; }

        public virtual RegistrationTable Register { get; set; }
        public virtual ICollection<CartTable> CartTable { get; set; }
        public virtual ICollection<OrderTable> OrderTable { get; set; }
    }
}
