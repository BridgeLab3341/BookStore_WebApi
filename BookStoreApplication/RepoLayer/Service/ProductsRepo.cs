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
    //Summary 
    //ProductRepo class contains Add Product by only admin to the bookstoreApplication
    //Constructor is Implemented with parameters and Dependencies are added from Configuration and DbBookContext file.
    //CRUD Operations are Performed in this class for Product Table.
    public class ProductsRepo : IProductsRepo
    {
        private readonly IConfiguration _configuration;
        private readonly dbBooksContext _booksContext;
        public ProductsRepo(IConfiguration configuration, dbBooksContext booksContext)
        {
            this._configuration = configuration;
            this._booksContext = booksContext;
        }
        //Summary
        //AddBooks Method is implemented to add books to the Product Table by Admin.
        public async Task<ProductTable> AddBooks(ProductModel model)
        {
            try
            {
                ProductTable table = new ProductTable(); //Creating the Instance of the ProductTable.
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
                await _booksContext.AddAsync(table);      //Adding data into dbcontext to database
                await _booksContext.SaveChangesAsync();   //save the data after adding to database
                return table;
            }
            catch (Exception)
            {
                throw new Exception("Products unable to add Product or Book Unsuccessful");
            }
        }
        //Summary
        //GetAll Products method is used to print All the Products present in the database of ProductTable
        //Printing the Products in console.
        public async Task<List<ProductTable>> GetAllProducts()  
        {
            try
            {
                List<ProductTable> products = new List<ProductTable>();  //Listing the Product Table to fetch Products in the table.
                products = await _booksContext.ProductTable.ToListAsync(); //Fetch the Products from dbcontext and Product Table.
                if (products != null)
                {
                    return products;   //Returning the Products into console.
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
        //Summary
        //Fetching the Products by Id
        //Printing the Products in console.
        public async Task<ProductTable> GetByProductId(long productId)
        {
            try
            {
                var table = await _booksContext.ProductTable.FirstOrDefaultAsync(x => x.ProductId == productId); //Fetch the Products from dbcontext and Product Table.
                if (table != null)
                {
                    return table;   //Returning the Products from Product Table.
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw new Exception("Failed");
            }
        }
        //Summary
        //Fetching the Product by Id to Update the Products
        //Updating the all the details of the Products and 
        //Returning the updated code in console.
        public async Task<ProductTable> UpdateProducts(UpdateProductsModel model, long productId)
        {
            try
            {
                var product = await _booksContext.ProductTable.FirstOrDefaultAsync(x => x.ProductId == productId); //Fetch the ProductId from dbcontext and Product Table.
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
                    await _booksContext.SaveChangesAsync(); //Saving the Updated data in to dbcontext of the table.
                    return product;    //Return the updated data in console.
                }
                return null;
            }
            catch (Exception)
            {
                throw new Exception("Updated Unsuccessfully");
            }
        }
        //Summary
        //Fetching the Product by Id to Delete the Products
        //Deleting the all the details of the Products by Product Id. 
        //Returning the Deleted Data in console.
        public async Task<ProductTable> DeleteProducts(long productId)
        {
            try
            {
                ProductTable product = new ProductTable();
                product = await _booksContext.ProductTable.FirstOrDefaultAsync(x => x.ProductId == productId);
                if (product != null)
                {
                    _booksContext.ProductTable.Remove(product); //Remove or delete that ProductId and Data inside of that id.
                    await _booksContext.SaveChangesAsync();  //save changes after delete in the dbcontext and table.
                    return product;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw new Exception("Unable to Delete Product or Book");
            }
        }
    }
}
