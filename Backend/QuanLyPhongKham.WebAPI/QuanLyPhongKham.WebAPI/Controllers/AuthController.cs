using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhongKham.Business.Interfaces;
using QuanLyPhongKham.Models.Models;

namespace QuanLyPhongKham.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Invalid client request" });
            }

            var result = await _authService.RegisterAsync(model);
            if (result is not null && result is not string)
            {
                return Ok(result); // Trả về thông báo thành công
            }

            return BadRequest(result); // Trả về thông báo lỗi
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Invalid client request" });
            }

            var result = await _authService.LoginAsync(model);
            if (result is not null && result is not string)
            {
                return Ok(result); // Trả về token và refresh token
            }

            return BadRequest(new { message = result });
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
        {
            if (tokenModel == null)
            {
                return BadRequest(new { message = "Invalid client request" });
            }

            var result = await _authService.RefreshTokenAsync(tokenModel);
            if (result.Status == "Success")
            {
                return Ok(result); // Trả về token mới và refresh token mới
            }

            return BadRequest(new { message = result });
        }
    }
}
