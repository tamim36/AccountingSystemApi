using AccountingSystemApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystemApi.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _dbContext;

        public TransactionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task GetTransactionById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertTransaction(Transaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public Task UpdateTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return await _dbContext.Transactions.ToListAsync();
        }
    }
}
