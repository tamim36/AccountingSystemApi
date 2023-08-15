using AccountingSystemApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystemApi.Repository
{
    public static class DummyData
    {
        public static void DataPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedClientData(serviceScope.ServiceProvider.GetService<AppDbContext>());
                SeedTransactionData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedClientData(AppDbContext dbContext)
        {
            if (!dbContext.Clients.Any())
            {
                Console.WriteLine("==> Seeding Data ...");

                dbContext.AddRange(
                        new Client() { Email = "test@gmail.com", Name="Test Client"}
                    );

                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("==> We already have data !");
            }
        }

        private static void SeedTransactionData(AppDbContext dbContext)
        {
            if (!dbContext.Transactions.Any())
            {
                Console.WriteLine("==> Seeding Data ...");

                var client = dbContext.Clients.First();
                dbContext.AddRange(
                        new Transaction() { Description = "Test 1", ClientId = client.ClientId, Amount = 20, Date = DateTime.UtcNow },
                        new Transaction() { Description = "Test 2", ClientId = client.ClientId, Amount = 30, Date = DateTime.UtcNow },
                        new Transaction() { Description = "Test 3", ClientId = client.ClientId, Amount = 40, Date = DateTime.UtcNow },
                        new Transaction() { Description = "Test 4", ClientId = client.ClientId, Amount = 50, Date = DateTime.UtcNow }
                    );

                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("==> We already have data !");
            }
        }
    }
}
