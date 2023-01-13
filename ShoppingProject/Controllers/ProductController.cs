using Business.Service.Interface;
using Data.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }
        [HttpGet(nameof(GetProduct) + "/{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var data = productService.Get(id);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet(nameof(GetAllProduct))]
        public IActionResult GetAllProduct()
        {
            try
            {
                var data = productService.GetAll();
                if (data == null || data.Count == 0) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet(nameof(SearchProduct))]
        public IActionResult SearchProduct(ProductDto model)
        {
            try
            {
                var data = productService.SearchByName(model.ProductName);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost(nameof(AddProduct))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "Admin")]
        public IActionResult AddProduct(ProductDto model)
        {
            try
            {
                if (model == null) return BadRequest();
                productService.Add(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut(nameof(UpdateProduct))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "Admin")]
        public IActionResult UpdateProduct(ProductDto model)
        {
            try
            {
                if (model == null) return BadRequest();
                productService.Update(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete(nameof(DeleteProduct))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "Admin")]
        public IActionResult DeleteProduct(ProductDto model)
        {
            try
            {
                if (model == null) return BadRequest();
                productService.Delete(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
