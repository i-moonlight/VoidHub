namespace ForumApi.Options
{
    public static class OptionsService
    {
        public static IServiceCollection AddAppOptions(this IServiceCollection services, IConfiguration config) 
        {
            services.AddOptions<JwtOptions>()
                .Bind(config.GetSection(JwtOptions.Jwt))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddOptions<ImageOptions>()
                .Bind(config.GetSection(ImageOptions.Image))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            return services;
        }        
    }
}