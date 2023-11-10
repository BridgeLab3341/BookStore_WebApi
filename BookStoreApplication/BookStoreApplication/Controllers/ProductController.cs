using BusinessLayer.Inteface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Summary
    //Declaring controller class with constructor.
    //Providing dependency from business class.
    //All the Method which are in this class Performs CRUD Operations.
    public class ProductController : ControllerBase
    {
        private readonly IProductsBusiness _productsBusiness;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductsBusiness productsBusiness, ILogger<ProductController> logger)
        {
            this._productsBusiness = productsBusiness;
            this._logger = logger;
        }
        //Summary
        //Authorizeing the method to Add Products or Books without any permission.
        //Declaring  Routing and Route Name
        //In this method We are Adding new Products or Books.
        [Authorize]
        [HttpPost]
        [Route("AddBooks")]
        public async Task<IActionResult> AddingBooks(ProductModel model)
        {
            try
            {
                //var Role = "Admin";
                var admin = (User.FindFirst("TypeofRegister").Value);
                _logger.LogInformation($"Admin '{admin}' is attempting to add books.");
                if (admin == "Admin")
                {
                    var result = await _productsBusiness.AddBooks(model);
                    if (result != null)
                    {
                        return Ok(new { success = true, message = "Books Added Successful", data = result });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Books not Added UnSuccessful" });
                    }
                }
                return BadRequest(new { success = false, message = "Books not Added UnSuccessful" });
            }
            catch (Exception ex)
            {
                //throw new Exception("Books not Added UnSuccessful");
                _logger.LogError(ex, "Error Found while adding books.");
                return BadRequest(new { success = false, message = "Books not Added Unsuccessful" });
            }
        }
        //Summary
        //We are not Authorized this method to Fetch all Products or Books.
        //Declaring  Routing and Route Name
        //In this method we Fetching the All the products or books without the Authorization.
        [HttpPost]
        [Route("GetAllBooks")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var result = await _productsBusiness.GetAllProducts();
                if (result != null)
                {
                    _logger.LogInformation($"Admin '{result}' is Fetching all books or Products.");
                    return Ok(new { success = true, message = "Retrieved All Products or Books Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to Retrieve All Products or Books UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Unable Retrieve All Products or Books UnSuccessful");
                _logger.LogError(ex, "Error Found while Fetching books.");
                return BadRequest(new { success = false, message = "Books not Found Unsuccessful" });
            }
        }
        [HttpPost]
        [Route("GetById")]
        public async Task<IActionResult> GetProductById(long productId)
        {
            try
            {
                var result = _productsBusiness.GetByProductId(productId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Retrieved Product or Book Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Unable to Retrieve Product or Book UnSuccessful" });
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Unable Retrieve Product or Book UnSuccessful");
                _logger.LogError(ex, "Error Found while Fetching book by Id.");
                //return BadRequest(new { success = false, message = "Books not Found Unsuccessful" });
                return NotFound();
            }
        }
        //Summary
        //We are Authorized this method to Update Products or Books.
        //Declaring  Routing and Route Name
        //In this method we Updating the products or books with the Authorization.
        [Authorize]
        [HttpPut]
        [Route("UpdateBooks/{productId}")]
        public async Task<IActionResult> UpdatingProducts(UpdateProductsModel model, long productId)
        {
            try
            {
                var admin = (User.FindFirst("TypeofRegister").Value);
                if (admin == "Admin")
                {
                    var result = await _productsBusiness.UpdateProducts(model, productId);
                    if (result != null)
                    {
                        return Ok(new { success = true, message = "Updated Products or Books Successfully", data = result });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Unable to Update Products or Books UnSuccessful" });
                    }
                }
                return BadRequest(new { success = false, message = "Unable to Update Products or Books UnSuccessful" });

            }
            catch (Exception ex)
            {
                //throw new Exception("Unable to Update Products or Books UnSuccessful");
                _logger.LogError(ex, "Error Found while Updating book by Id.");
                return BadRequest(new { success = false, message = "Unable to Update Products or Books UnSuccessful" });
            }
        }

        //Summary
        //We are Authorized this method to Update Products or Books.
        //Declaring  Routing and Route Name
        //In this method we Deleting the products or books with the Authorization.
        [Authorize]
        [HttpDelete]
        [Route("RemoveBooks")]
        public async Task<IActionResult> DeletingProducts(long productId)
        {
            try
            {
                //var registerId = long.Parse(User.FindFirst("RegisterId").Value);
                var admin = (User.FindFirst("TypeofRegister").Value);
                if (admin == "Admin")
                {
                    var result = await _productsBusiness.DeleteProducts(productId);
                    if (result != null)
                    {
                        return Ok(new { success = true, message = "Deleted Products or Books Successfully" });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Unable to Delete Products or Books UnSuccessful" });
                    }
                }
                return BadRequest(new { success = false, message = "Unable to Remove Products or Books UnSuccessful" });
            }
            catch (Exception ex)
            {
                //throw new Exception("Unable to Delete Products or Books UnSuccessful");
                _logger.LogError(ex, "Error Found while Deleting book by Id.");
                return BadRequest(new { success = false, message = "Unable to Remove Products or Books UnSuccessful" });
            }
        }

    }
}
