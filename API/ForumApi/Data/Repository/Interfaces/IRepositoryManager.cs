namespace ForumApi.Data.Repository.Interfaces
{
    public interface IRepositoryManager
    {
        Lazy<IAccountRepository> Account { get; }
        Lazy<ITokenRepository> Token { get; }
        Lazy<ISectionRepository> Section { get; }
        Lazy<IForumRepository> Forum { get; }
        Lazy<ITopicRepository> Topic { get; }
        Lazy<IPostRepository> Post { get; }
        Lazy<IBanRepository> Ban { get; }

        Task BeginTransaction();
        Task Commit();
        Task Rollback();
        Task Save();
    }
}