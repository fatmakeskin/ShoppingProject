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
    public class OrderController : ControllerBase
    {
        private IOrderService orderService;
        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService;
        }
        [HttpGet(nameof(GetOrder) + "/{id}")]
        public IActionResult GetOrder(int id)
        {
            try
            {
                var data = orderService.Get(id);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet(nameof(GetAllOrder) + "/{id}")]
        public IActionResult GetAllOrder()
        {
            try
            {
                var data = orderService.GetAll();
                if (data == null || data.Count == 0) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost(nameof(AddOrder))]
        public IActionResult AddOrder(OrderDto model)
        {
            try
            {
                if (model == null) return BadRequest();
                orderService.Add(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut(nameof(UpdateOrder))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "Admin")]
        public IActionResult UpdateOrder(OrderDto model)
        {
            try
            {
                if (model == null) return BadRequest();
                orderService.Update(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete(nameof(DeleteOrder))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "Admin")]
        public IActionResult DeleteOrder(OrderDto model)
        {
            try
            {
                if (model == null) return BadRequest();
                orderService.Delete(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
