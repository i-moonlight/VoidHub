using ForumApi.Data.Repository.Interfaces;

namespace ForumApi.Data.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ForumDbContext _context;

        public IAccountRepository Account { get; }
        public ITokenRepository Token { get; }

        public ISectionRepository Section { get; }
        public IForumRepository Forum { get; }
        public ITopicRepository Topic { get; }
        public IPostRepository Post { get; }

        public RepositoryManager(
            ForumDbContext context,
            IAccountRepository account, 
            ITokenRepository token,
            ISectionRepository section,
            IForumRepository forum,
            ITopicRepository topic,
            IPostRepository post)
        {
            _context = context;
            Account = account;
            Token = token;

            Section = section;
            Forum = forum;
            Topic = topic;
            Post = post;
        }

        public async Task BeginTransaction() => 
            await _context.Database.BeginTransactionAsync();
        
        public async Task Commit() => 
            await _context.Database.CommitTransactionAsync();

        public async Task Rollback() => 
            await _context.Database.RollbackTransactionAsync();

        public async Task Save() => 
            await _context.SaveChangesAsync();
    }
}