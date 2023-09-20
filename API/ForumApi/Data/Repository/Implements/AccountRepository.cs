using ForumApi.Data.Models;
using ForumApi.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Data.Repository.Implements
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(ForumDbContext context) : base(context)
        {
        }

        public IQueryable<Account> FindByLogin(string login, bool asTracking = false) =>
            FindByCondition(a => a.LoginName == login, asTracking);

        public IQueryable<Account> FindByLoginWithTokens(string login, bool asTracking = false) => 
            FindByLogin(login, asTracking).Include(a => a.Tokens);

        public override void Delete(Account entity)
        {
            entity.DeletedAt = System.DateTime.Now;
            entity.Email += "-deleted";
        }
    }
}