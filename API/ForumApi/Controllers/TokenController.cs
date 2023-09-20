using ForumApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [ApiController]
    [Route("api/v1/tokens")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }        

        [HttpDelete("{token}")]
        public async Task<IActionResult> Revoke(string token)
        {
            await _tokenService.Revoke(token);
            return Ok();
        }
    }
}