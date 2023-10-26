using ForumApi.Data.Models;
using ForumApi.Data.Repository.Interfaces;

namespace ForumApi.Data.Repository.Implements
{
    public class BanRepository : RepositoryBase<Ban>, IBanRepository
    {
        public BanRepository(ForumDbContext context) : base(context)
        {}

        public override void Delete(Ban entity)
        {
            entity.IsActive = false;
        }
    }
}