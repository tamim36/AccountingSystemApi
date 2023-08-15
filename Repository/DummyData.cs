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
                SeedTransactionData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedTransactionData(AppDbContext dbContext)
        {
            if (!dbContext.Transactions.Any())
            {
                Console.WriteLine("==> Seeding Data ...");

                dbContext.AddRange(
                        new Transaction() { Description = "Test 1", Amount = 20, Date = DateTime.UtcNow },
                        new Transaction() { Description = "Test 2", Amount = 30, Date = DateTime.UtcNow },
                        new Transaction() { Description = "Test 3", Amount = 40, Date = DateTime.UtcNow },
                        new Transaction() { Description = "Test 4", Amount = 50, Date = DateTime.UtcNow }
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
