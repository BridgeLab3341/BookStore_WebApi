﻿using BusinessLayer.Inteface;
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
using RepoLayer.Context;
using Microsoft.Extensions.Logging;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Summary
    //Declaring controller class with constructor.
    //Providing dependency from business class.
    //All the Method which are in this class Performs Registration and Login Operations.
    public class AdminController : ControllerBase
    {
        private readonly IRegistrationBusiness _regiBusiness;
        private readonly ILogger<AdminController> _logger;
        public AdminController(IRegistrationBusiness business, ILogger<AdminController> logger)
        {
            this._regiBusiness = business;
            this._logger = logger;
        }
        //Summary
        //We are Authorized this method for Registration Operation only Default admin can Access.
        //Declaring  Routing and Route Name
        //In this method we are doing Admin Registration Authorization.
        [Authorize]
        [HttpPost]
        [Route("AdminRegistration")]
        public async Task<IActionResult> AdminRegistration(RegistrationModel model)
        {
            try
            {

                var admin = (User.FindFirst("RegistrationId").Value);
                if (admin == "Admin")
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
                return BadRequest(new { success = false, message = "Your not Authorized to Register" });
            }
            catch (Exception ex)
            {
                //throw new Exception("Registration failed");
                _logger.LogError(ex, "Error Found Registration UnSuccessful.");
                return BadRequest(new { success = false, message = "Registration UnSuccessful" });
            }
        }
        //Summary
        //We are UnAuthorized this method for Login Operation.
        //Declaring  Routing and Route Name.
        //In this method we are doing Admin Login Operation.
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> AdminLogin(Login login)
        {
            try
            {
                var result = await _regiBusiness.Login(login);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Admin Login Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Admin Login UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Registration failed");
                _logger.LogError(ex, "Error Found, Admin Login UnSuccessful.");
                return BadRequest(new { success = false, message = "Admin Login UnSuccessful" });
            }
        }
    }
}
