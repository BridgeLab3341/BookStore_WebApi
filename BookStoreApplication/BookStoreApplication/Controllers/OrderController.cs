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
    //Summary
    //Declaring controller class with constructor.
    //Providing dependency from business class.
    //All the Method which are in this class Performs Add and GetAll Operations.
    //Authorized for all methods.
    //Routing is provided for all methods.
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderBusiness orderBusiness, ILogger<OrderController> logger)
        {
            this._orderBusiness = orderBusiness;
            this._logger = logger;
        }
        //Summary
        //In this methods we are Placing the Order using the registration Id ,ProductID and CustomersDetailsId for Customer.
        //Claiming the Registration Id using the claim method.
        //Claiming the TypeofRegister to Perform the operation if he/she is only customer.
        //Implemented Loggers for throwing wrror message.
        //Implemented Exception handling to throw errors.
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
                _logger.LogError(ex, "Error Found Placing Order UnSuccessful.");
                return BadRequest(new { success = false, message = "Placing Order UnSuccessful" });
            }
        }
        //Summary
        //In this methods we are Fetching all Orders Placed by Customer.
        //Claiming the TypeofRegister to Perform the operation if he/she is only customer.
        //Implemented Loggers for throwing error message.
        //Implemented Exception handling to throw errors.
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
