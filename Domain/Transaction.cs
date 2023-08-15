namespace AccountingSystemApi.Domain
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }

}
