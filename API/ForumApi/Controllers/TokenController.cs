using ForumApi.Data.Models;
using ForumApi.Filters;
using ForumApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [PermissionActionFilter<Token>]
        public async Task<IActionResult> Revoke(string token)
        {
            await _tokenService.Revoke(token);
            return Ok();
        }
    }
}