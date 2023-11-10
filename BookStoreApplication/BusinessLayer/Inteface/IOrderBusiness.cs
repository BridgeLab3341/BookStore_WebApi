using CommonLayer.Models;
using RepoLayer.Context.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Inteface
{
    public interface IOrderBusiness
    {
        public Task<OrderTable> PlaceOrder(OrderModel model, long productId, long registrationId, long customerDetailsId);
        public Task<List<OrderTable>> GetAllOrders();
    }
}
