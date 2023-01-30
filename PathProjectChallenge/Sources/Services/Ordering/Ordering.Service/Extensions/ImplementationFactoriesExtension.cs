using Microsoft.Extensions.DependencyInjection;
using Ordering.Core.Repositories;
using Ordering.Data.Repositories.MongoRepositories;
using Ordering.Data.Repositories.SqlServerRepositories;

namespace Ordering.Service.Extensions
{
    public static class ImplementationFactoriesExtension
    {
        public static void AddMultipleRepositories(this IServiceCollection services)
        {
            #region Sql Server Repositories
            services.AddScoped<OrderSqlServerRepository>();
            #endregion

            #region Mongo Database Repositories
            services.AddScoped<OrderMongoRepository>();

            #endregion

            #region Implementation Factories
            // IOrderRepository Implementation Factory
            services.AddScoped(implementationFactory =>
            {
                Func<string, IOrderRepository> accessor = key =>
                {
                    if (key.Equals("SqlServer"))
                    {
                        return implementationFactory.GetService<OrderSqlServerRepository>();
                    }
                    else if (key.Equals("MongoDb"))
                    {
                        return implementationFactory.GetService<OrderMongoRepository>();
                    }
                    else
                    {
                        throw new ArgumentException($"Not Support key : {key}");
                    }
                };
                return accessor;
            });

            #endregion

        }

    }
}
