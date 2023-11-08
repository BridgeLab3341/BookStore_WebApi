using BusinessLayer.Inteface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging.Abstractions;
using RepoLayer.Context.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IRegistrationBusiness _regiBusiness;
        public AdminController(IRegistrationBusiness business)
        {
            this._regiBusiness = business;
        }
        [HttpPost]
        [Route("AdminRegistration")]
        public async Task<IActionResult> AdminRegistration(RegistrationModel model)
        {
            try
            {
                var result = await _regiBusiness.Registration(model);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration UnSuccessful" });
                }
            }
            catch (Exception)
            {
                throw new Exception("Registration failed");
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> AdminLogin(Login login)
        {
            try
            {
                var result = await _regiBusiness.Login(login);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration UnSuccessful" });
                }
            }
            catch (Exception)
            {
                throw new Exception("Login Failed");
            }
        }
    }
}
