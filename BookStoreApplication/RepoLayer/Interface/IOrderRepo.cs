using CommonLayer.Models;
using RepoLayer.Context.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    //Implemented Interfaces for OrderRepo 
    public interface IOrderRepo
    {
        public Task<OrderTable> PlaceOrder(OrderModel model, long productId, long registrationId, long customerDetailsId);
        public Task<List<OrderTable>> GetAllOrders();
    }
}
