namespace ForumApi.Options
{
    public static class OptionsService
    {
        public static IServiceCollection AddAppOptions(this IServiceCollection services, IConfiguration config) 
        {
            services.AddOptions<JwtOptions>().Bind(config.GetSection(JwtOptions.Jwt))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services;
        }        
    }
}