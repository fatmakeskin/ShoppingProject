using Business.Service.Interface;
using Business.Service.Services;
using Data.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private IWalletService walletService;
        public WalletController(IWalletService _walletService)
        {
            walletService = _walletService;
        }
        [HttpGet(nameof(GetWallet) + "/{id}")]
        public IActionResult GetWallet(int id)
        {
            try
            {
                var data = walletService.GetSummary(id);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost(nameof(AddMoney))]
        public IActionResult AddMoney(WalletDto model)
        {
            try
            {
                if (model.Money == 0) return BadRequest();
                walletService.AddMoney(model);
                var oldMoney = walletService.GetSummary(model.UserId);
                if (oldMoney.First().Money > 0)
                    walletService.AddMoney(model);
                else
                    walletService.UpdateMoney(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
