using FluentValidation;
using FluentValidation.AspNetCore;
using ForumApi.DTO.Auth;
using ForumApi.DTO.Page;
using Microsoft.AspNetCore.Builder;

namespace ForumApi.Extensions
{
    public static class ValidatorService
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(fv => 
            {
                fv.DisableDataAnnotationsValidation = true;
            });
            
            services.AddScoped<IValidator<Page>, PageValidator>();

            services.AddScoped<IValidator<Register>, RegisterValidator>();
            services.AddScoped<IValidator<Login>, LoginValidator>();

            return services;
        }
    }
}