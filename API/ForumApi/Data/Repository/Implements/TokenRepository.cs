using ForumApi.Data.Models;
using ForumApi.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Data.Repository.Implements
{
    public class TokenRepository : RepositoryBase<Token>, ITokenRepository
    {
        public TokenRepository(ForumDbContext context) : base(context)
        {
        }

        public IQueryable<Token> FindByToken(string refreshToken, bool asTracking = false) => 
            FindByCondition(t => t.RefreshToken == refreshToken, asTracking);

        public IQueryable<Token> FindByTokenWithAccount(string refreshToken, bool asTracking = false) => 
            FindByToken(refreshToken, asTracking).Include(t => t.Account);
    }
}