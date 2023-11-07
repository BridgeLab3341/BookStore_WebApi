using BusinessLayer.Inteface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationBusiness _regiBusiness;
        public RegistrationController(IRegistrationBusiness business)
        {
            this._regiBusiness = business;
        }
        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Register(RegistrationModel model)
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
        public async Task<IActionResult> LoginUser(Login login)
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
