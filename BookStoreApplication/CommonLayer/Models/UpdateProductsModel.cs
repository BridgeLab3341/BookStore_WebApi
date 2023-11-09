using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class UpdateProductsModel
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal Price { get; set; }
    }
}
