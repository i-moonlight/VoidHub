using FluentValidation;
using ForumApi.Data.Models;
using ForumApi.DTO.Auth;
using ForumApi.DTO.DAccount;
using ForumApi.Extensions;
using ForumApi.Filters;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            return Ok(await _accountService.Get(id));
        }

        [HttpPatch]
        [Authorize]
        [BanFilter]
        public async Task<IActionResult> UpdateSelf(AccountDto accountDto)
        {
            var validator = new AccountDtoValidator();
            await validator.ValidateAndThrowAsync(accountDto);

            var senderId = User.GetId();
            return Ok(await _accountService.Update(senderId, senderId, accountDto));
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = Role.Admin)]
        [BanFilter]
        public async Task<IActionResult> ChangeRole(int id, AccountDto accountDto)
        {
            Console.WriteLine(accountDto.Role);
            var validator = new AccountDtoAdminValidator();
            await validator.ValidateAndThrowAsync(accountDto);

            await _accountService.Update(id, User.GetId(), accountDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        [BanFilter]
        public async Task<IActionResult> DeleteAccount(int id) 
        {
            await _accountService.Delete(User.GetId());
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteSelf()
        {
            await _accountService.Delete(User.GetId());
            return Ok();
        }
    }
}