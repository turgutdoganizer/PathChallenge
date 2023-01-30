using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace Ordering.Data.Contexts.EntityFrameworkCore
{
    public class OrderingContextFactory : IDesignTimeDbContextFactory<OrderingContext>
    {
        public OrderingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderingContext>();
            optionsBuilder.UseSqlServer("Server=tcp:futureformative.database.windows.net,1433;Initial Catalog=FF-Ordering;Persist Security Info=False;User ID=futureformative;Password=1234567?!Aa;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
                optionsBuilder =>
                {
                    optionsBuilder.MigrationsAssembly(typeof(OrderingContext).GetTypeInfo().Assembly.GetName().Name);
                    optionsBuilder.EnableRetryOnFailure(5);
                });
            return new OrderingContext(optionsBuilder.Options);
        }
    }


}

