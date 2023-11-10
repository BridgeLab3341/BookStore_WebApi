using CommonLayer.Models;
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
        private readonly IConfiguration _configuration;
        private readonly dbBooksContext _dbBooksContext;
        public OrderRepo(IConfiguration configuration, dbBooksContext booksContext)
        {
            this._configuration = configuration;
            this._dbBooksContext = booksContext;
        }
        public async Task<OrderTable> PlaceOrder(OrderModel model,long productId,long registrationId,long customerDetailsId)
        {
            try
            {
                var result = await _dbBooksContext.OrderTable.FirstOrDefaultAsync(x => x.ProductId == productId);
                if (result != null)
                {
                    result.OrderTime = model.OrderTime;
                    result.Quantity=result.Quantity-model.Quantity;
                    result.Amount = model.Quantity * result.Amount;
                    result.CustomerDetailId=customerDetailsId;
                    result.ProductId = productId;
                    result.RegisterId = registrationId;
                    await _dbBooksContext.OrderTable.AddAsync(result);
                    _dbBooksContext.SaveChanges();
                    return result;
                }
                return null;
            }
            catch (Exception)
            {
                throw new Exception("Order Failed");
            }
        }
        public async Task<List<OrderTable>> GetAllOrders()
        {
            try
            {
                List<OrderTable> orders = new List<OrderTable>();
                orders = await _dbBooksContext.OrderTable.ToListAsync();
                if(orders != null)
                {
                    return orders;
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
