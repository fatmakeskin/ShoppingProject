using Business.Service.Interface;
using Data.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private IBasketService basketService;
        public BasketController(IBasketService _basketService)
        {
            basketService = _basketService;
        }
        [HttpGet(nameof(GetBasket) + "/{id}")]
        public IActionResult GetBasket(int id)
        {
            try
            {
                var data = basketService.Get(id);
                if (data == null) BadRequest();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet(nameof(GetAllBasket))]
        public IActionResult GetAllBasket()
        {
            try
            {
                var data = basketService.GetAll();
                if (data == null || data.Count == 0) BadRequest();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost(nameof(AddBasket))]
        public IActionResult AddBasket(BasketDto model)
        {
            try
            {
                if (model == null) return BadRequest();
                basketService.Add(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut(nameof(UpdateBasket))]
        public IActionResult UpdateBasket(BasketDto model)
        {
            try
            {
                if (model == null) return BadRequest();
                basketService.Update(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete(nameof(DeleteBasket))]
        public IActionResult DeleteBasket(BasketDto model)
        {
            try
            {
                if (model == null) return BadRequest();
                basketService.Delete(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
