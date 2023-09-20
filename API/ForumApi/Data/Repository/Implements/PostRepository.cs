using ForumApi.Data.Models;
using ForumApi.Data.Repository.Interfaces;

namespace ForumApi.Data.Repository.Implements
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(ForumDbContext context) : base(context)
        {
        }
    }
}