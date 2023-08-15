using AccountingSystemApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountingSystemApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public ReportController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetAllFinancialReports([FromRoute] int clientId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var report = await _transactionService.GenerateFinancialReport(clientId, startDate, endDate);

            if (report == null)
                return NotFound();

            return Ok(report);
        }
    }
}
