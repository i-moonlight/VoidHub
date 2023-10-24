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
                options.UseLazyLoadingProxies().UseNpgsql(config.GetConnectionString("ForumDb")));

            services.AddScoped<ITokenRepository, TokenRepository>()
                .AddScoped(provider => new Lazy<ITokenRepository>(() => provider.GetRequiredService<ITokenRepository>()));
            services.AddScoped<IAccountRepository, AccountRepository>()
                .AddScoped(provider => new Lazy<IAccountRepository>(() => provider.GetRequiredService<IAccountRepository>()));

            services.AddScoped<ISectionRepository, SectionRepository>()
                .AddScoped(provider => new Lazy<ISectionRepository>(() => provider.GetRequiredService<ISectionRepository>()));
            services.AddScoped<ITopicRepository, TopicRepository>()
                .AddScoped(provider => new Lazy<ITopicRepository>(() => provider.GetRequiredService<ITopicRepository>()));
            services.AddScoped<IPostRepository, PostRepository>()
                .AddScoped(provider => new Lazy<IPostRepository>(() => provider.GetRequiredService<IPostRepository>()));
            services.AddScoped<IForumRepository, ForumRepository>()
                .AddScoped(provider => new Lazy<IForumRepository>(() => provider.GetRequiredService<IForumRepository>()));

            services.AddScoped<IRepositoryManager, RepositoryManager>();

            return services;
        }        
    }
}