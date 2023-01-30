using Microsoft.Extensions.Options;
using Ordering.Core.Data.Mongo;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Data.Repositories.Base;

namespace Ordering.Data.Repositories.MongoRepositories
{
    public class OrderMongoRepository : MongoRepository<Order>, IOrderRepository
    {
        public OrderMongoRepository(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}
