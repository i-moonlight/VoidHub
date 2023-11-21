using ForumApi.Data.Repository.Interfaces;
using ForumApi.DTO.DAccount;
using ForumApi.Exceptions;
using ForumApi.Extensions;
using ForumApi.Options;
using ForumApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ForumApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepositoryManager _rep;
        private readonly JwtOptions _jwtOptions;
        private readonly ITokenService _tokenService;

        public AccountService(
            IRepositoryManager rep,
            IOptions<JwtOptions> jwtOptions,
            ITokenService tokenService)
        {
            _rep = rep;
            _jwtOptions = jwtOptions.Value;
            _tokenService = tokenService;
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

        public async Task Update(int id, AccountDto accountDto)
        {
            var user = await _rep.Account.Value.FindById(id, true)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("User with such id doesn't exist");

            if(!string.IsNullOrEmpty(accountDto.Username))
                user.Username = accountDto.Username;

            if(!string.IsNullOrEmpty(accountDto.Email))
                user.Email = accountDto.Email;
            
            if(accountDto.Role != Data.Models.RoleEnum.None)
                user.Role = accountDto.Role.ToString();

            if(!string.IsNullOrEmpty(accountDto.OldPassword) && !string.IsNullOrEmpty(accountDto.NewPassword))
            {
                if(!PasswordHelper.Verify(user.PasswordHash, accountDto.OldPassword))
                    throw new BadRequestException("Old password is incorrect");

                user.PasswordHash = PasswordHelper.Hash(accountDto.NewPassword);
            }

            await _rep.Save();
        }
    }
}