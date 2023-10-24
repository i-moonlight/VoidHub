using AutoMapper;
using ForumApi.Data.Models;
using ForumApi.Data.Repository.Interfaces;
using ForumApi.DTO.Auth;
using ForumApi.Exceptions;
using ForumApi.Extensions;
using ForumApi.Options;
using ForumApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ForumApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryManager _rep;
        private readonly ITokenService _tokenService;
        private readonly JwtOptions _jwtOptions;
        private readonly IMapper _mapper;

        public AuthService(
            IRepositoryManager rep,
            ITokenService tokenService,
            IOptions<JwtOptions> jwtOptions,
            IMapper mapper)
        {
            _rep = rep;
            _tokenService = tokenService;
            _jwtOptions = jwtOptions.Value;
            _mapper = mapper;
        }
        
        public async Task<AuthResponse> RefreshPair(string refreshToken)
        {
            var tokenEntity = await _rep.Token.Value.FindByTokenWithAccount(refreshToken, true)
                .FirstOrDefaultAsync() ??  throw new NotFoundException("Invalid refresh token");

            if(tokenEntity.Account.DeletedAt != null)
                throw new BadRequestException("You cannot refresh token for deleted account");

            if(tokenEntity.ExpiresAt < DateTime.UtcNow)
                throw new BadRequestException("Refresh token expired");

            var pair = _tokenService.CreatePair(tokenEntity.Account);

            tokenEntity.RefreshToken = pair.RefreshToken;
            tokenEntity.ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshLifetimeInMinutes);

            //update last logged date
            tokenEntity.Account.LastLoggedAt = DateTime.UtcNow;

            await _rep.Save();

            return new AuthResponse
            {
                Tokens = pair,
                User = _mapper.Map<User>(tokenEntity.Account)
            };
        }

        public async Task<AuthResponse> Login(Login auth)
        {
            var account = await _rep.Account.Value
                .FindByLoginWithTokens(auth.LoginName, true)
                .FirstOrDefaultAsync() ?? throw new BadRequestException("User with such login doesn't exist");

            if(account.DeletedAt != null)
                throw new BadRequestException("User with such login doesn't exist");

            if(!PasswordHelper.Verify(auth.Password, account.PasswordHash))
                throw new BadRequestException("Password is incorrect");

            //check max tokens count and update
            if(account.Tokens.Count > _jwtOptions.MaxTokenCount)
            {
                var token = account.Tokens.OrderBy(t => t.ExpiresAt).First();
                _rep.Token.Value.Delete(token);
            }

            var newPair = _tokenService.CreatePair(account);

            account.Tokens.Add(new Token
            {
                AccountId = account.Id,
                RefreshToken = newPair.RefreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshLifetimeInMinutes)
            });

            account.LastLoggedAt = DateTime.UtcNow;

            await _rep.Save();

            return new AuthResponse
            {
                Tokens = newPair,
                User = _mapper.Map<User>(account)
            };
        }

        public async Task<AuthResponse> Register(Register auth)
        {
            if(await _rep.Account.Value.FindByLogin(auth.LoginName).AnyAsync())
                throw new BadRequestException("User with such login already exists");

            if(await _rep.Account.Value.FindByCondition(a=>a.Email == auth.Email).AnyAsync())
                throw new BadRequestException("User with such email already exists");

            var account = new Account()
            {
                Username = auth.Username,
                LoginName = auth.LoginName,
                Email = auth.Email,
                PasswordHash = PasswordHelper.Hash(auth.Password),
                Role = Role.User
            };

            //transaction needed for token generation, because neeeds account id
            await _rep.BeginTransaction();
            try 
            {
                _rep.Account.Value.Create(account);
                await _rep.Save();

                var pair = _tokenService.CreatePair(account);

                account.Tokens.Add(new Token
                {
                    AccountId = account.Id,
                    RefreshToken = pair.RefreshToken,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.RefreshLifetimeInMinutes)
                });

                await _rep.Save();
                await _rep.Commit();

                return new AuthResponse
                {
                    Tokens = pair,
                    User = _mapper.Map<User>(account)
                };
            } 
            catch 
            {
                await _rep.Rollback();
                throw; 
            }
        }
    }
}