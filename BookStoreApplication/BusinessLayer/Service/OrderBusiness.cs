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
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderRepo _orderRepo;
        public OrderBusiness(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public async Task<OrderTable> PlaceOrder(OrderModel model, long productId, long registrationId, long customerDetailsId)
        {
            try
            {
                return await _orderRepo.PlaceOrder(model,productId,registrationId,customerDetailsId);
            }
            catch (Exception)
            {
                throw new Exception("Order failed");
            }
        }
        public async Task<List<OrderTable>> GetAllOrders()
        {
            try
            {
                return await _orderRepo.GetAllOrders();
            }
            catch (Exception)
            {
                throw new Exception("Fetching All Orders Failed");
            }
        }
    }
}
