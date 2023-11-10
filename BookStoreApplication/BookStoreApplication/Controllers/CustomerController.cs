using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using BusinessLayer.Inteface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Summary
    //Declaring controller class with constructor.
    //Providing dependency from business class.
    //All the Method which are in this class Performs Registration, Login, Forgot and Reset Operations.
    public class CustomerController : ControllerBase
    {
        private readonly IRegistrationBusiness _regiBusiness;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(IRegistrationBusiness business, ILogger<CustomerController> logger)
        {
            this._regiBusiness = business;
            this._logger = logger;
        }
        //Summary
        //We are UnAuthorized this method for Customer Registration.
        //In this method we are doing Customer Registration.
        [HttpPost]
        [Route("CustomerRegistration")]
        public async Task<IActionResult> CustomerRegistration(RegistrationModel model)
        {
            try
            {

                var result = await _regiBusiness.Registration(model);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Customer Registration Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Customer Registration UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Registration failed");
                _logger.LogError(ex, "Error Found Customer Registration UnSuccessful.");
                return BadRequest(new { success = false, message = "Customer Registration UnSuccessful" });
            }
        }
        //Summary
        //We are UnAuthorized this method for Customer Login.
        //In this method we are doing Customer Login.
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser(Login login)
        {
            try
            {
                var result = await _regiBusiness.Login(login);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Customer Login Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Customer Login UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Registration failed");
                _logger.LogError(ex, "Error Found Customer Login UnSuccessful.");
                return BadRequest(new { success = false, message = "Customer Login UnSuccessful" });
            }
        }
        //Summary
        //We are Authorized this method for Forgot Password Operation and particular Customer can Access.
        //Declaring  Routing and Route Name
        //In this method we are doing Customer Forget password Operation with Authorization.
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPass(string email)
        {
            try
            {
                var result = await _regiBusiness.ForgotPassword(email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Customer Data Found Token Sent Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Customer Data NotFound Token NotSent UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Registration failed");
                _logger.LogError(ex, "Error Found Token Not Sent, UnSuccessful.");
                return BadRequest(new { success = false, message = "Token NotSent UnSuccessful" });
            }
        }
        //Summary
        //We are Authorized this method for Reset Password Operation and particular Customer can Access.
        //Declaring  Routing and Route Name
        //In this method we are doing Reset password Operation with Authorization.
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPass(string password, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = _regiBusiness.ResetPassword(email, password, confirmPassword);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Reset Password Successfully", /*data = result*/ });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset Password UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Registration failed");
                _logger.LogError(ex, "Error Found TReset Password UnSuccessful.");
                return BadRequest(new { success = false, message = "Reset Password UnSuccessful" });
            }
        }
    }
}
