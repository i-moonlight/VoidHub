using System.Linq.Dynamic.Core;
using ForumApi.Data;
using ForumApi.Data.Repository.Extensions;
using ForumApi.Exceptions;
using ForumApi.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Filters
{
    public class PermissionActionFilter<T> : ActionFilterAttribute where T : class
    {
        private readonly string _columnName = "AccountId";

        public PermissionActionFilter(string columnName)
        {
            _columnName = columnName;
        }

        public PermissionActionFilter(){}

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = context.HttpContext.User.GetId();

            if (!context.ActionArguments.ContainsKey("id"))
                throw new ArgumentNullException("Id is not provided");
            
            if(!int.TryParse(context.ActionArguments["id"]?.ToString(), out int entityId))
                throw new BadRequestException("Id is not valid");

            var db = context.HttpContext.RequestServices.GetService<ForumDbContext>()
                ?? throw new Exception("Can't get db context");

            _ = await db.Set<T>()
                .EnableAsTracking(false)
                .Where($"Id == {entityId} and {_columnName} == {userId}")
                .FirstOrDefaultAsync() ?? throw new ForbiddenException("You don't have permission to do this action");

            await next();
        }
    }
}