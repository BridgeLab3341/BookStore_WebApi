﻿using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using BusinessLayer.Inteface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRegistrationBusiness _regiBusiness;
        public CustomerController(IRegistrationBusiness business)
        {
            this._regiBusiness = business;
        }
        [HttpPost]
        [Route("CustomerRegistration")]
        public async Task<IActionResult> CustomerRegistration(RegistrationModel model)
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
                    return Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login UnSuccessful" });
                }
            }
            catch (Exception)
            {
                throw new Exception("Login Failed");
            }
        }
        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPass(string email)
        {
            try
            {
                var result = await _regiBusiness.ForgotPassword(email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "User Data Found Token Sent Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "User Data NotFound Token NotSent UnSuccessfully" });
                }
            }
            catch (Exception)
            {
                throw new Exception("Reset Password Failed");
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPass(string password, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value.ToString();
                var result = _regiBusiness.ResetPassword(email, password, confirmPassword);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Reset Password Successfully", /*data = result*/ });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset Password UnSuccessfully" });
                }
            }
            catch (Exception)
            {
                throw new Exception("Reset Password Failed");
            }
        }

    }
}