using AutoMapper;
using ForumApi.Data.Repository.Interfaces;
using ForumApi.DTO.Auth;
using ForumApi.DTO.DAccount;
using ForumApi.DTO.DBan;
using ForumApi.Exceptions;
using ForumApi.Extensions;
using ForumApi.Options;
using ForumApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;

namespace ForumApi.Services
{
    public class AccountService 
        (
            IRepositoryManager _rep, 
            IMapper _mapper, 
            IOptions<ImageOptions> imageOptions
        ) : IAccountService
    {
        private readonly ImageOptions _imageOptions = imageOptions.Value;

        public async Task<AccountResponse> Get(int id)
        {
            return await _rep.Account.Value
                .FindByCondition(a => a.Id == id)
                .Select(a => new AccountResponse
                {
                    Id = a.Id,
                    Username = a.Username,
                    Role = a.Role,
                    AvatarPath = a.AvatarPath,
                    CreatedAt = a.CreatedAt,
                    PostsCount = a.Posts.Count(p => p.AccountId == a.Id && p.DeletedAt == null),
                    TopicsCount = a.Topics.Count(t => t.AccountId == a.Id && t.DeletedAt == null),
                    Ban = a.RecievedBans.Where(b => b.IsActive && b.ExpiresAt > DateTime.UtcNow)
                        .OrderByDescending(b => b.IsActive)
                        .ThenByDescending(b => b.ExpiresAt)
                        .Select(b => _mapper.Map<BanDto>(b))
                        .FirstOrDefault()
                })
                .FirstOrDefaultAsync() ?? throw new NotFoundException("User with such id doesn't exist");
        }

        public async Task Delete(int id)
        {
            var account = await _rep.Account.Value
                .FindByCondition(a => a.Id == id, true)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("User with such id doesn't exist");

            _rep.Account.Value.Delete(account);
            account.Tokens.Clear();

            await _rep.Save();
        }

        public async Task<AuthUser> Update(int targetId, int senderId, AccountDto accountDto)
        {
            var user = await _rep.Account.Value.FindById(targetId, true)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("User with such id doesn't exist");

            if(!string.IsNullOrEmpty(accountDto.Username))
                user.Username = accountDto.Username;

            if(!string.IsNullOrEmpty(accountDto.Email))
                user.Email = accountDto.Email;
            
            if(accountDto.Role != Data.Models.RoleEnum.None)
            {
                if(targetId == senderId)
                    throw new BadRequestException("You cannot change your own role");

                user.Role = accountDto.Role.ToString();
            }

            if(!string.IsNullOrEmpty(accountDto.OldPassword) && !string.IsNullOrEmpty(accountDto.NewPassword))
            {
                if(accountDto.OldPassword == accountDto.NewPassword)
                    throw new BadRequestException("New password is the same as old");

                if(!PasswordHelper.Verify(accountDto.OldPassword, user.PasswordHash))
                    throw new BadRequestException("Old password is incorrect");

                user.PasswordHash = PasswordHelper.Hash(accountDto.NewPassword);
            }
            
            await _rep.Save();

            return _mapper.Map<AuthUser>(user);
        }

        public async Task<AuthUser> UpdateImg(int accountId, string newPath)
        {
            var user = await _rep.Account.Value.FindById(accountId, true)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("User not found");

            user.AvatarPath = newPath;
            await _rep.Save();

            return _mapper.Map<AuthUser>(user);
        }
    }
}