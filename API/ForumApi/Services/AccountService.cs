using ForumApi.Data.Repository.Interfaces;
using ForumApi.Exceptions;
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
            var account = await _rep.Account
                .FindByCondition(a => a.Id == id, true)
                .Include(a => a.Tokens)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("User with such id doesn't exist");

            _rep.Account.Delete(account);
            account.Tokens.Clear();

            await _rep.Save();
        }
    }
}