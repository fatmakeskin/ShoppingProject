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
    public class CategoryController : ControllerBase
    {
        private ICategoryService categoryService;
        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }
        [HttpGet(nameof(GetCategory) + "/{id}")]
        public IActionResult GetCategory(int id)
        {
            try
            {
                var data = categoryService.Get(id);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet(nameof(GetAllCategory))]
        public IActionResult GetAllCategory()
        {
            try
            {
                var data = categoryService.GetAll();
                if (data == null || data.Count == 0) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost(nameof(AddCategory))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "Admin")]
        public IActionResult AddCategory(CategoryDto model)
        {
            try
            {
                if (model == null) return BadRequest();
                categoryService.Add(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut(nameof(UpdateCategory))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "Admin")]
        public IActionResult UpdateCategory(CategoryDto model)
        {
            try
            {
                if (model == null) return BadRequest();
                categoryService.Update(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete(nameof(DeleteCategory))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "Admin")]
        public IActionResult DeleteCategory(CategoryDto model)
        {
            try
            {
                if (model == null) return BadRequest();
                categoryService.Delete(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
