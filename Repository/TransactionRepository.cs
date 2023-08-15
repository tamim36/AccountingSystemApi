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

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _dbContext.Transactions.FirstOrDefaultAsync(x => x.TransactionId == id);
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

        public async Task UpdateTransaction(Transaction transaction)
        {
            var existingTransaction = await GetTransactionById(transaction.TransactionId);
            if (existingTransaction != null)
            {
                existingTransaction.Description = transaction.Description;
                existingTransaction.Amount = transaction.Amount;
                
                _dbContext.Transactions.Update(existingTransaction);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task DeleteTransaction(Transaction transaction)
        {
            _dbContext.Transactions.Remove(transaction);
            await _dbContext.SaveChangesAsync();
        }
    }
}
