using Ordering.Core.Entities.Base;
using System.Linq.Expressions;

namespace Ordering.Core.Repositories.Base
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = null);
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeString = null,
            bool disableTracking = true);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null,
            bool disableTracking = true);

        Task<IReadOnlyList<T>> GetAllPaginationAsync(int pageNumber, int pageSize);

        Task<int> CountAsync();

        Task<int> CountFilterAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(Guid id);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate);
        Task<T> DeleteAsync(T entity);
        Task<T> DeleteAsync(Expression<Func<T, bool>> predicate);

    }
}
