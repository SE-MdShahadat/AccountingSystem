using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AccountingSystemWebAPI.Data;
using AccountingSystemWebAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace AccountingSystemWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            var transactions = await _context.Transactions.ToListAsync();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.TransactionId }, transaction);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTransaction(int id, Transaction transaction)
        //{
        //    if (id != transaction.TransactionId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(transaction).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, Transaction updatedTransaction)
        {
            var existingTransaction = await _context.Transactions.FindAsync(id);

            if (existingTransaction == null)
            {
                return NotFound();
            }

            if (!existingTransaction.RowVersion.SequenceEqual(updatedTransaction.RowVersion))
            {
                return Conflict(); // Return conflict response if versions don't match
            }

            _context.Entry(existingTransaction).State = EntityState.Detached;
            _context.Entry(updatedTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(); // Handle concurrency conflict due to parallel updates
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
