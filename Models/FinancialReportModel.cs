namespace AccountingSystemApi.Models
{
    public class FinancialReportModel
    {
        public string ClientName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal ProfitLoss { get; set; }
    }
}
