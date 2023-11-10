using BusinessLayer.Inteface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderBusiness orderBusiness, ILogger<OrderController> logger)
        {
            this._orderBusiness = orderBusiness;
            this._logger = logger;
        }
        [Authorize]
        [HttpPost]
        [Route("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder(OrderModel model, long productId, long customerDetailsId)
        {
            try
            {
                var id = long.Parse(User.FindFirst("RegistrationId").Value);
                var cus = User.FindFirst("TypeofRegister").Value;
                if (cus == "Customer")
                {
                    var result = await _orderBusiness.PlaceOrder(model, productId, id, customerDetailsId);
                    if (result != null)
                    {
                        return Ok(new { success = true, message = "Order Placed Successful", data = result });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Placing Order failed UnSuccessful" });
                    }
                }
                return BadRequest(new { success = false, message = "Placing Order failed UnSuccessful" });
            }
            catch (Exception ex)
            {
                //throw new Exception("Registration failed");
                _logger.LogError(ex, "Error Found Placing Order UnSuccessful.");
                return BadRequest(new { success = false, message = "Placing Order UnSuccessful" });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetAllOrder")]
        public async Task<IActionResult> FetchAllOrders()
        {
            try
            {
                var cus = User.FindFirst("TypeofRegister").Value;
                if (cus == "Customer")
                {
                    var result = _orderBusiness.GetAllOrders();
                    if(result != null)
                    {
                        return Ok(new { success = true, message = "Orders Fetched Successfully", data = result });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = " Fetching Orders failed UnSuccessful" });
                    }
                }
                return BadRequest(new { success = false, message = "Fetching Orders failed UnSuccessful" });
            }
            catch (Exception ex)
            {
                //throw new Exception("Registration failed");
                _logger.LogError(ex, "Error Found Fetching Orders UnSuccessful.");
                return BadRequest(new { success = false, message = "Fetching Orders UnSuccessful" });
            }

        }
    }
}
