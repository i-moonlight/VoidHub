using FluentValidation;
using ForumApi.Data.Models;
using ForumApi.DTO.DAccount;
using ForumApi.Extensions;
using ForumApi.Filters;
using ForumApi.Options;
using ForumApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ForumApi.Controllers
{
    [ApiController]
    [Route("api/v1/accounts")]
    public class AccountController(IAccountService _accountService, IOptions<ImageOptions> options, IImageService _imageService) : ControllerBase
    {   
        private readonly ImageOptions _imageOptions = options.Value;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            return Ok(await _accountService.Get(id));
        }

        [HttpPatch]
        [Authorize]
        [BanFilter]
        public async Task<IActionResult> UpdateSelf([FromBody] AccountDto accountDto)
        {
            var validator = new AccountDtoValidator();
            await validator.ValidateAndThrowAsync(accountDto);

            var senderId = User.GetId();
            return Ok(await _accountService.Update(senderId, senderId, accountDto));
        }

        [HttpPatch("avatar")]
        [Authorize]
        [BanFilter]
        public async Task<IActionResult> UpdateImageSelf(AccountDto accountDto, [FromQuery] string currentPath)
        {
            var validator = new AccountDtoImageValidator();
            await validator.ValidateAndThrowAsync(accountDto);

            var avatarPath = $"{_imageOptions.AvatarFolder}/{User.GetId()}.png";
            var fullPath = Path.Combine(_imageOptions.Folder, avatarPath);

            var image = _imageService.PrepareImage(accountDto.Img);
            await _imageService.SaveImage(image, fullPath);

            // avatar path always id.format (except default), so we can not to update it every time
            if(string.IsNullOrEmpty(currentPath) || currentPath != avatarPath)
                return Ok(await _accountService.UpdateImg(User.GetId(), avatarPath));
            
            return Ok();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = Role.Admin)]
        [BanFilter]
        public async Task<IActionResult> ChangeRole(int id, AccountDto accountDto)
        {
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