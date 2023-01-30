using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities.Base;
using Ordering.Core.Repositories.Base;
using Ordering.Data.Contexts.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ordering.Data.Repositories.Base
{
    public class SqlServerRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly OrderingContext _orderingContext;

        public SqlServerRepository(OrderingContext orderingContext)
        {
            _orderingContext = orderingContext ?? throw new ArgumentNullException(nameof(orderingContext));
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _orderingContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await _orderingContext.Set<T>().Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _orderingContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _orderingContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }


        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await _orderingContext.Set<T>().FindAsync(id);
            if (entity != null)
                _orderingContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }


        public async Task<T> AddAsync(T entity)
        {
            await _orderingContext.Set<T>().AddAsync(entity);
            await _orderingContext.SaveChangesAsync();
            _orderingContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _orderingContext.Entry(entity).State = EntityState.Modified;
            await _orderingContext.SaveChangesAsync();
            _orderingContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            var insertedEntity = await GetByIdAsync(entity.Id);
            if (insertedEntity != null)
                _orderingContext.Set<T>().Remove(entity);
            int influencing = await _orderingContext.SaveChangesAsync();
            _orderingContext.Entry(entity).State = EntityState.Detached;
            if (influencing > 0)
            {
                return insertedEntity;
            }
            return insertedEntity;
        }

        public async Task<IReadOnlyList<T>> GetAllPaginationAsync(int pageNumber, int pageSize)
        {
            var pagedData = await _orderingContext.Set<T>().Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            return pagedData;
        }

        public async Task<int> CountAsync()
        {
            return await _orderingContext.Set<T>().CountAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _orderingContext.Set<T>().Where(predicate).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> CountFilterAsync(Expression<Func<T, bool>> predicate)
        {
            var data = await _orderingContext.Set<T>().Where(predicate).CountAsync();
            return data;
        }

        public Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
