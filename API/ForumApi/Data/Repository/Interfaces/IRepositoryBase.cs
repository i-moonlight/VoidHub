using System.Linq.Expressions;

namespace ForumApi.Data.Repository.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool asTracking = false);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool asTracking = false);
        T Create(T entity);
        void Delete(T entity);
        void DeleteMany(IEnumerable<T> entities);        
    }
}