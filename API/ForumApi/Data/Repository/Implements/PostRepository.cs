using ForumApi.Data.Models;
using ForumApi.Data.Repository.Interfaces;

namespace ForumApi.Data.Repository.Implements
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(ForumDbContext context) : base(context)
        {
        }

        public override void Delete(Post entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            DeleteMany(entity.Comments);
        }

        public override void DeleteMany(IEnumerable<Post> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }
    }
}