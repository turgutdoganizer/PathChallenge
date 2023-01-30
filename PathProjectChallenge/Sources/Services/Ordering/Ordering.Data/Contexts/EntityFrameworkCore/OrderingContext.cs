using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;

namespace Ordering.Data.Contexts.EntityFrameworkCore
{
    /// <summary>
    /// Ordering Context
    /// </summary>
    public class OrderingContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public OrderingContext(DbContextOptions<OrderingContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Order> Orders { get; set; }

    }
}
