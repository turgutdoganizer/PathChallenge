using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;


namespace Ordering.Data.Contexts.EntityFrameworkCore
{
    public class OrderingContextSeed
    {
        public static async Task SeedAsync(OrderingContext orderContext, ILogger<OrderingContextSeed> logger)
        {
            //if (!orderContext.Orders.Any())
            //{
            //    orderContext.Orders.AddRange(GetPreconfiguredOrders());
            //    await orderContext.SaveChangesAsync();
            //    logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderingContext).Name);
            //}
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                //new Order() {UserName = "swn", FirstName = "Turgut", LastName = "İzer", EmailAddress = "turgut.izer@gmail.com", AddressLine = "İzmir", Country = "Turkey", TotalPrice = 350 }
            };
        }
    }
}
