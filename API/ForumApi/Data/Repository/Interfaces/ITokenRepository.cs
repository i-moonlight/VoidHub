using ForumApi.Data.Models;

namespace ForumApi.Data.Repository.Interfaces
{
    public interface ITokenRepository : IRepositoryBase<Token>
    {
        IQueryable<Token> FindByTokenWithAccount(string refreshToken, bool asTracking = false);
        IQueryable<Token> FindByToken(string refreshToken, bool asTracking = false);
    }
}