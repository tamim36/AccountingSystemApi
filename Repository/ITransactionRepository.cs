using AccountingSystemApi.Domain;

namespace AccountingSystemApi.Repository
{
    public interface ITransactionRepository
    {
        Task InsertTransaction(Transaction transaction);

        Task UpdateTransaction(Transaction transaction);

        Task GetTransactionById(int id);

        Task<IEnumerable<Transaction>> GetAllTransactions();
    }
}
