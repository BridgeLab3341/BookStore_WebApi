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
    public class ProductsBusiness : IProductsBusiness
    {
        private readonly IProductsRepo _productsRepo;
        public ProductsBusiness(IProductsRepo productsRepo)
        {
            this._productsRepo = productsRepo;
        }
        public async Task<ProductTable> AddBooks(ProductModel model)
        {
            try
            {
                return await _productsRepo.AddBooks(model);
            }
            catch (Exception)
            {
                throw new Exception(" Books Not Added Unsuccessful");
            }
        }
        public async Task<List<ProductTable>> GetAllProducts()
        {
            try
            {
                return await _productsRepo.GetAllProducts();
            }
            catch (Exception)
            {
                throw new Exception("Retrieved All Products");
            }
        }

        public async Task<ProductTable> UpdateProducts(UpdateProductsModel model, long productId)
        {
            try
            {
                return await _productsRepo.UpdateProducts(model, productId);
            }
            catch (Exception)
            {
                throw new Exception("Unable to Updat Product or Books ");
            }
        }
        public Task<ProductTable> DeleteProducts(long productId)
        {
            try
            {
                return _productsRepo.DeleteProducts(productId);
            }
            catch (Exception)
            {
                throw new Exception("Unable to Delete Product or Book");
            }
        }
        public Task<ProductTable> GetByProductId(long productId)
        {
            try
            {
                return _productsRepo.GetByProductId(productId);
            }
            catch (Exception)
            {
                throw new Exception("failed");
            }
        }
    }
}
