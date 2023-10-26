using ForumApi.Data.Repository.Interfaces;
using ForumApi.Exceptions;
using ForumApi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Middlewares
{
    public class BanMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        private readonly List<string> CheckedMethods = new()
        {
            "POST",
            "PUT",
            "PATCH",
            "DELETE"
        };

        public BanMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            
        }

        public async Task InvokeAsync(HttpContext context, Lazy<IBanRepository> banRepo)
        {
            if(CheckedMethods.Contains(context.Request.Method))
                await CheckForBans(context, banRepo);

            await _next(context);
        }

        private async Task CheckForBans(HttpContext context, Lazy<IBanRepository> banRepo)
        {
            var userId = 0;

            //skip if user is not authenticated (probably resgiter&login endpoints)
            try
            {
                userId = context.User.GetId();
            }
            catch (ArgumentNullException)
            {
                return;
            }

            var userBans = await banRepo.Value
                .FindByCondition(b => b.AccountId == userId && ((b.IsActive && b.ExpiresAt > DateTime.UtcNow) || (b.IsActive && b.IsPermanent)))
                .OrderByDescending(b => b.IsPermanent)
                .ThenByDescending(b => b.ExpiresAt)
                .FirstOrDefaultAsync();

            if (userBans != null)
            {
                if (userBans.IsPermanent)
                    throw new ForbiddenException("You are banned permanently");
                else
                    throw new ForbiddenException($"You are banned until {userBans.ExpiresAt}");
            }
        }

    }
}