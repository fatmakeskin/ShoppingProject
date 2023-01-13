using Business.Authentication;
using Business.Service.Interface;
using Data.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "Admin")]
    public class UserController : ControllerBase
    {
        IUserService userService;
        TokenProvider token;
        public UserController(IUserService _userService, TokenProvider _token)
        {
            userService = _userService;
            token = _token;
        }
        [HttpGet(nameof(GetUser) + "/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "Admin,Member")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var data = userService.Get(id);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet(nameof(GetAllUser))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "Admin")]
        public IActionResult GetAllUser()
        {
            try
            {
                var data = userService.GetAll();
                if (data == null || data.Count == 0) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost(nameof(AddUser))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "Admin")]
        public IActionResult AddUser(UserDto model)
        {
            try
            {
                if (model == null) return BadRequest();
                userService.Add(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut(nameof(UpdateUser))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "Admin")]
        public IActionResult UpdateUser(UserDto model)
        {
            try
            {
                if (model == null) return BadRequest();
                userService.Update(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete(nameof(DeleteUser))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "Admin")]
        public IActionResult DeleteUser(UserDto model)
        {
            try
            {
                if (model == null) return BadRequest();
                userService.Delete(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost(nameof(SignUp))]
        [AllowAnonymous]
        public IActionResult SignUp(UserDto model)
        {
            try
            {
                if (model == null) return BadRequest();

                var data = userService.SignUp(model);

                if (data == null) return BadRequest("Kullanıcı Adı zaten var.");

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost(nameof(Login))]
        [AllowAnonymous]
        public IActionResult Login(LoginDto model)
        {
            try
            {
                if (model == null) return BadRequest();

                var data = userService.Login(model);

                if (data == null) return Unauthorized();

                return Ok(data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost(nameof(ChangePassword))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "GetAccess", Roles = "Member")]
        public IActionResult ChangePassword(ChangePassword model)
        {
            try
            {
                if (model == null) return BadRequest();
                userService.ChangePassword(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
