using ForumApi.DTO.Auth;
using ForumApi.Services;
using ForumApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(
            IAuthService authService
        )
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Register dto) 
        {
            return Ok(await _authService.Register(dto));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login dto) 
        {
            return Ok(await _authService.Login(dto));
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> Refresh(string refreshToken)
        {
            return Ok(await _authService.RefreshPair(refreshToken));
        }
    }
}