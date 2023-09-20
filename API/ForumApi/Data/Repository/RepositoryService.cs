using ForumApi.Data.Repository.Implements;
using ForumApi.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Data.Repository
{
    public static class RepositoryService
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ForumDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("ForumDb")));

            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IForumRepository, ForumRepository>();

            services.AddScoped<IRepositoryManager, RepositoryManager>();

            return services;
        }        
    }
}