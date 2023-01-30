using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Ordering.Core.Data.Mongo;
using Ordering.Core.Entities.Base;
using Ordering.Core.Repositories.Base;
using System.Linq.Expressions;

namespace Ordering.Data.Repositories.Base
{
    public class MongoRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly IMongoCollection<T> Collection;
        protected readonly MongoDbSettings settings;

        public MongoRepository(IOptions<MongoDbSettings> options)
        {
            this.settings = options.Value;
            var client = new MongoClient(this.settings.ConnectionString);
            var db = client.GetDatabase(this.settings.Database);
            this.Collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
        }

        public async Task<T> AddAsync(T entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await Collection.InsertOneAsync(entity, options);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return await Collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity);
        }


        public async Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate)
        {
            return await Collection.FindOneAndReplaceAsync(predicate, entity);
        }

        public async Task<T> DeleteAsync(T entity)
        {
            return await Collection.FindOneAndDeleteAsync(x => x.Id == entity.Id);
        }

        public async Task<T> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            return await Collection.FindOneAndDeleteAsync(predicate);
        }
        public IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null
              ? Collection.AsQueryable()
              : Collection.AsQueryable().Where(predicate);
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = Collection.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            return await query.ToListAsync();
        }


        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = Collection.AsQueryable();
            if (disableTracking)
                query = query.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(includeString))
                query = query.Include(includeString);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = Collection.AsQueryable();
            if (disableTracking)
                query = query.AsNoTracking();
            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            if (predicate != null)
                query = query.Where(predicate);
            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await Collection.Find(predicate).FirstOrDefaultAsync();
        }


        public async Task<int> CountAsync()
        {
            return Collection.Aggregate().ToListAsync().Result.Count;
        }

        public Task<int> CountFilterAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }


        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await Collection.Aggregate().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllPaginationAsync(int pageNumber, int pageSize)
        {



            //var filter = Builders<T>.Filter.Eq(x => x.FirstName, "Bob");

            var data = await Collection.Aggregate()
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

















            //var pagedData = await _orderingContext.Set<T>().Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            return data;
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }


    }
}
