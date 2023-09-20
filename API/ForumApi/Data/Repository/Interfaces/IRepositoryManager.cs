namespace ForumApi.Data.Repository.Interfaces
{
    public interface IRepositoryManager
    {
        IAccountRepository Account { get; }
        ITokenRepository Token { get; }
        ISectionRepository Section { get; }
        IForumRepository Forum { get; }
        ITopicRepository Topic { get; }
        IPostRepository Post { get; }

        Task BeginTransaction();
        Task Commit();
        Task Rollback();
        Task Save();
    }
}