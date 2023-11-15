using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Context.Models;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class OrderRepo :IOrderRepo
    {
        //Summary 
        //OrderRepo class contains Place Order and Fetch the Orders in bookstoreApplication.
        //Constructor is Implemented with parameters and Dependencies are added from Configuration and DbBookContext file.
        //Interface is Implemented to add Dependencies to the class.
        private readonly IConfiguration _configuration;
        private readonly dbBooksContext _dbBooksContext;
        public OrderRepo(IConfiguration configuration, dbBooksContext booksContext)
        {
            this._configuration = configuration;
            this._dbBooksContext = booksContext;
        }
        //Summary
        //PlaceOrder method is implemented to place order of Products or Book to buy.
        //Placing the Order by using the ProductId,registrationId,CustomerDetailsId.
        public async Task<OrderTable> PlaceOrder(OrderModel model,long productId,long registrationId,long customerDetailsId)
        {
            try
            {
                OrderTable order = new OrderTable();  //Creating the Instance of the OrderTable.
                order.OrderTime = model.OrderTime;
                order.Quantity = model.Quantity;
                //order.Amount = model.Quantity * order.Amount;
                order.CustomerDetailId = customerDetailsId;
                order.ProductId = productId;
                order.RegisterId = registrationId;
                await _dbBooksContext.AddAsync(order);  //Add the Placed Order Items and Details of the Product or Book into dbContext of table.
                //order.Quantity -=model.Quantity;
                _dbBooksContext.SaveChanges();   //save the changes 
                return order;
            }
            catch (Exception)
            {
                throw new Exception("Order Failed");
            }
        }
        //Summary
        //Fetch or Get All Placed Orders and Return in the console.
        
        public async Task<List<OrderTable>> GetAllOrders()
        {
            try
            {
                List<OrderTable> orders = new List<OrderTable>(); //List the OrderTable to Fetch the All Details in the OrderTable.
                orders = await _dbBooksContext.OrderTable.ToListAsync(); // List out the Products from the dbContext order table.
                if(orders != null)
                {
                    return orders; //returning the data in console.
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw new Exception("Fetching all Orders Failed");
            }
        }
    }
}
