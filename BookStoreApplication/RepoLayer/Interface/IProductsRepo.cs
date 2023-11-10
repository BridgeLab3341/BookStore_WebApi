using CommonLayer.Models;
using RepoLayer.Context.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface IProductsRepo
    {
        public Task<ProductTable> AddBooks(ProductModel model);
        public Task<List<ProductTable>> GetAllProducts();
        public Task<ProductTable> UpdateProducts(UpdateProductsModel model, long productId);
        public Task<ProductTable> GetByProductId(long productId);
        public Task<ProductTable> DeleteProducts(long productId);
    }
}
