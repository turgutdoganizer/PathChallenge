using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Data.Contexts.EntityFrameworkCore;
using Ordering.Data.Repositories.Base;

namespace Ordering.Data.Repositories.SqlServerRepositories
{
    public class OrderSqlServerRepository : SqlServerRepository<Order>, IOrderRepository
    {
        public OrderSqlServerRepository(OrderingContext orderingContext) : base(orderingContext)
        {
        }
    }
}
