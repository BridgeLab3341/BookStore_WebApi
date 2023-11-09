using CommonLayer.Models;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Context.Models;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class ProductsRepo : IProductsRepo
    {
        private readonly IConfiguration _configuration;
        private readonly dbBooksContext _booksContext;
        public ProductsRepo(IConfiguration configuration, dbBooksContext booksContext)
        {
            this._configuration = configuration;
            this._booksContext = booksContext;
        }
        public async Task<ProductTable> AddBooks(ProductModel model)
        {
            try
            {
                ProductTable table = new ProductTable();
                table.BookName = model.BookName;
                table.Author = model.Author;
                table.Language = model.Language;
                table.Image = model.Image;
                table.Descrption = model.Description;
                table.Quantity = model.Quantity;
                table.Status = model.Status;
                table.Discountprice = model.DiscountPrice;
                table.Price = model.Price;
                //table.RegisterId = model.RegisterId;
                await _booksContext.AddAsync(table);
                await _booksContext.SaveChangesAsync();
                return table;
            }
            catch(Exception)
            {
                throw new Exception("Products unable to add Product or Book Unsuccessful");
            }
        }
        public async Task<List<ProductTable>> GetAllProducts()
        {
            try
            {
                List<ProductTable> products = new List<ProductTable>();
                products = await _booksContext.ProductTable.ToListAsync();
                if(products!=null)
                {
                    return products;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<ProductTable> GetByProductId(long productId)
        {
            try
            {
                var table = await _booksContext.ProductTable.FirstOrDefaultAsync(x => x.ProductId == productId);
                if(table!=null)
                {
                    return table;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception)
            {
                throw new Exception("Failed");
            }
        }
        public async Task<ProductTable> UpdateProducts(UpdateProductsModel model, long productId)
        {
            try
            {
               var product = await _booksContext.ProductTable.FirstOrDefaultAsync(x=>x.ProductId == productId);
                if (product != null)
                {
                    product.BookName = model.BookName;
                    product.Author = model.Author;
                    product.Language = model.Language;
                    product.Image = model.Image;
                    product.Descrption = model.Description;
                    product.Quantity = model.Quantity;
                    product.Status = model.Status;
                    product.Discountprice = model.DiscountPrice;
                    product.Price = model.Price;
                    await _booksContext.SaveChangesAsync();
                    return product;
                }
                    return null;
            }
            catch(Exception)
            {
                throw new Exception("Updated Unsuccessfully");
            }
        }
        public async Task<ProductTable> DeleteProducts(long productId)
        {
            try
            {
                ProductTable product = new ProductTable();
                product = await _booksContext.ProductTable.FirstOrDefaultAsync(x => x.ProductId == productId);
                if (product != null)
                {
                    _booksContext.ProductTable.Remove(product);
                    await _booksContext.SaveChangesAsync();
                    return product;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception)
            {
                throw new Exception("Unable to Delete Product or Book");
            }
        }
    }
}
