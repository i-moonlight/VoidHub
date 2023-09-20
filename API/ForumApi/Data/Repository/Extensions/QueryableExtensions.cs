using ForumApi.DTO.Page;
using Microsoft.EntityFrameworkCore;

namespace ForumApi.Data.Repository.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> EnableAsTracking<T>(this IQueryable<T> query, bool asTracking)
            where T: class =>
            asTracking ? query.AsTracking() : query.AsNoTracking();

        public static IQueryable<T> TakePage<T>(this IQueryable<T> query, Page page)
            where T : class => 
            query.Skip((page.PageNumber - 1) * page.PageSize)
            .Take(page.PageSize);
    }
}