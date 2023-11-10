using BusinessLayer.Inteface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepoLayer.Context.Models;
using System;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderBusiness orderBusiness, Logger<OrderController> logger)
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
                var cus = User.FindFirst("TypeofRegister").Value;
                if (cus == "Customer")
                {
                    var id = long.Parse(User.FindFirst("RegistrationId").Value);
                    var result = await _orderBusiness.PlaceOrder(model,productId,id,customerDetailsId);
                    if (result != null)
                    {
                        return Ok(new { success = true, message = "Books Added Successful", data = result });
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
                _logger.LogError(ex, "Error Found Registration UnSuccessful.");
                return BadRequest(new { success = false, message = "Registration UnSuccessful" });
            }
        }
    }
}
