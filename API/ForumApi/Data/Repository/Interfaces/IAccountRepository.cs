using ForumApi.Data.Models;

namespace ForumApi.Data.Repository.Interfaces
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        IQueryable<Account> FindByLogin(string login, bool asTracking = false);
        IQueryable<Account> FindByLoginWithTokens(string login, bool asTracking = false);
        /// <summary>
        /// Set DeletedAt time to current time and change email to email-deleted
        /// <para>Not saving db changes</para>
        /// </summary>
        new void Delete(Account entity);
    }
}