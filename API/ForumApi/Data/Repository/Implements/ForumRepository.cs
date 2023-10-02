using ForumApi.Data.Models;
using ForumApi.Data.Repository.Interfaces;

namespace ForumApi.Data.Repository.Implements
{
    public class ForumRepository : RepositoryBase<Forum>, IForumRepository
    {
        public ForumRepository(ForumDbContext context) : base(context)
        {
        }

        public override void Delete(Forum entity)
        {
            entity.DeletedAt = System.DateTime.Now;
        }

        public override void DeleteMany(IEnumerable<Forum> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }
    }
}