using AccountingSystemApi.Models;

namespace AccountingSystemApi.Services
{
    public interface ITransactionService
    {
        Task<FinancialReportModel> GenerateFinancialReport(int clientId, DateTime startDate, DateTime endDate);
    }
}
