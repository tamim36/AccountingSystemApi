using AccountingSystemApi.Domain;
using AccountingSystemApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _transactionRepository.GetAllTransactions();
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> InsertTransaction(Transaction transaction)
        {
            await _transactionRepository.InsertTransaction(transaction);
            return Ok(transaction);
        }
    }
}
