using ForumApi.Data.Models;
using ForumApi.DTO.Auth;
using ForumApi.Extensions;
using ForumApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [ApiController]
    [Route("api/v1/accounts")]
    public class AccountController : ControllerBase
    {   
        private readonly IAccountService _accountService;

        public AccountController(
            IAccountService accountService
        )
        {
            _accountService = accountService;
        }

        [HttpDelete("{id}"), Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteAccount(int id) 
        {
            await _accountService.Delete(User.GetId());
            return Ok();
        }

        [HttpDelete, Authorize]
        public async Task<IActionResult> DeleteSelf()
        {
            await _accountService.Delete(User.GetId());
            return Ok();
        }
    }
}