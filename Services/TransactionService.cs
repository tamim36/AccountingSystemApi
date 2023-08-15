using AccountingSystemApi.Models;
using AccountingSystemApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystemApi.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _dbcontext;

        public TransactionService(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<FinancialReportModel> GenerateFinancialReport(int clientId, 
            DateTime startDate, DateTime endDate)
        {
            var client = await _dbcontext.Clients.FirstOrDefaultAsync(x => x.ClientId == clientId);

            if (client == null)
            {
                return null;
            }
            var transactions = _dbcontext.Transactions
            .Where(t => t.Date >= startDate && t.Date <= endDate && t.ClientId == clientId)
            .ToList();

            if (!transactions.Any())
            {
                return null;
            }

            decimal totalIncome = transactions.Where(t => t.Amount > 0)
                                        .Sum(t => t.Amount);
            decimal totalExpenses = transactions.Where(t => t.Amount < 0)
                                        .Sum(t => t.Amount);

            decimal profitLoss = totalIncome + totalExpenses;

            var report = new FinancialReportModel
            {
                ClientName = client.Name,
                StartDate = startDate,
                EndDate = endDate,
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                ProfitLoss = profitLoss
            };

            return report;
        }
    }
}
