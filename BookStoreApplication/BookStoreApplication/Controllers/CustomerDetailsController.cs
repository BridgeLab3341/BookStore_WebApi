using BusinessLayer.Inteface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepoLayer.Context.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        
        private readonly ICustomerDetailsBusiness _customerDetails;
        private readonly ILogger<ProductController> _logger;
        public CustomerDetailsController(ICustomerDetailsBusiness customerDetails, ILogger<ProductController> logger)
        {
            this._customerDetails = customerDetails;
            _logger = logger;
        }
        [Authorize]
        [HttpPost]
        [Route("AddCustomerDetails")]
        public async Task<IActionResult> AddCustomersDetails(CustomerDetailsModel model)
        {
            try
            {
                var id =long.Parse(User.FindFirst("RegistrationId").Value);
                var admin = (User.FindFirst("TypeofRegister").Value);
                if(admin == "Customer")
                {
                    var result = await _customerDetails.AddCustomerDetails(model, id);
                    if (result != null)
                    {
                        return Ok(new { success = true, message = "Customer Details Added Successfully", data = result });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Customer Details not Added UnSuccessful" });
                    }
                }
                return BadRequest(new { success = false, message = "Customer Details not Added UnSuccessful" });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error Found while adding Customer Details.");
                return BadRequest(new { success = false, message = "Customer Details not Added Unsuccessful"});
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetCustomerDetails")]
        public async Task<IActionResult> FetchAllCustomerDetails()
        {
            try
            {
                var admin = (User.FindFirst("TypeofRegister").Value);
                if (admin == "Customer")
                {
                    var result = await _customerDetails.GetAllCustomerDetails();
                    if (result != null)
                    {
                        return Ok(new { success = true, message = "Fetched Customer Details Successfully", data = result });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Unable to Fetched Customer Details UnSuccessful" });
                    }
                }
                return BadRequest(new { success = false, message = "Customer Details not Found UnSuccessful" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Found while Fetching Customer Details.");
                return BadRequest(new { success = false, message = "Customer Details not Found Unsuccessful" });
            }
        }
    }
}
