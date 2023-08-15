using AccountingSystemWebAPI.Data;
using AccountingSystemWebAPI.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystemWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("financial")]
        public async Task<ActionResult<FinancialReport>> GenerateFinancialReport(int clientId, DateTime startDate, DateTime endDate)
        {
            var transactions = await _context.Transactions
                .Where(t => t.Date >= startDate && t.Date <= endDate && t.ClientId == clientId)
                .ToListAsync();

            decimal totalIncome = transactions.Where(t => t.Amount > 0).Sum(t => t.Amount);
            decimal totalExpenses = transactions.Where(t => t.Amount < 0).Sum(t => t.Amount);
            decimal profitLoss = totalIncome + totalExpenses;

            var report = new FinancialReport
            {
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                ProfitLoss = profitLoss
                // Initialize other report fields as needed
            };

            return Ok(report);
        }
    }
}
