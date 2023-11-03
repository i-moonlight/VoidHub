using System.Linq.Expressions;

namespace ForumApi.Data.Repository.Extensions
{
    public static class PredicateBuilderExtensions
    {
        /// <summary>
        /// Combines the first predicate with the second using the logical "and" if first and "or" in other cases.
        /// </summary>
        public static Expression<Func<T, bool>> AndOrFirst<T>(this Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> predicator, ref bool first)
        {
            if(first)
            {
                first = false;
                return predicate.AND(predicator);
            }
            else
            {
                return predicate.OR(predicator);
            }
        }
    }
}