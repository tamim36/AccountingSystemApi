using AccountingSystemApi.Domain;
using AccountingSystemApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace AccountingSystemApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly AppDbContext _dbContext;

        public TransactionController(ITransactionRepository transactionRepository, AppDbContext dbContext)
        {
            _transactionRepository = transactionRepository;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionRepository.GetAllTransactions();
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> InsertTransaction(Domain.Transaction transaction)
        {
            using var transactionScope = new TransactionScope(TransactionScopeOption.Required,
                                                         new TransactionOptions { IsolationLevel = IsolationLevel.Serializable });

            try
            {
                var existingTransaction = _dbContext.Transactions.Find(transaction.TransactionId);
                if (existingTransaction != null)
                {
                    return Conflict("Concurrency conflict");
                }

                await _transactionRepository.InsertTransaction(transaction);

                transactionScope.Complete();
                return Ok("Created");
            }
            catch (Exception)
            {
                transactionScope.Dispose();
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction([FromRoute] int id)
        {
            var transaction = await _transactionRepository.GetTransactionById(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction([FromRoute] int id, [FromBody] Domain.Transaction transaction)
        {
            if (id != transaction.TransactionId)
            {
                return BadRequest();
            }

            await _transactionRepository.UpdateTransaction(transaction);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int id)
        {
            var transaction = await _transactionRepository.GetTransactionById(id);

            if (transaction == null)
            {
                return NotFound();
            }

            await _transactionRepository.DeleteTransaction(transaction);

            return Ok();
        }
    }
}
