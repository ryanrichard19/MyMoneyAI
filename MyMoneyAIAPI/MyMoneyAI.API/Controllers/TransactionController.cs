using Microsoft.AspNetCore.Mvc;
using MyMoneyAI.Application.Interfaces;
using MyMoneyAI.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyMoneyAI.Application.DTOs;

namespace MyMoneyAI.API.Controllers
{
    [ApiController]
    [Route("api/transactions"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme) ]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/transactions
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionService.ListAsync();
            return Ok(transactions);
        }

        // GET: api/transactions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _transactionService.FindByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        // POST: api/transactions
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionDto transactionDto)
        {
            var newTransactionDto = await _transactionService.AddAsync(transactionDto);
            return CreatedAtAction(nameof(GetTransactionById), new { id = newTransactionDto.Id }, newTransactionDto);
        }

        // PUT: api/transactions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] TransactionDto updatedTransactionDto)
        {
            if (id != updatedTransactionDto.Id)
            {
                return BadRequest();
            }

            await _transactionService.UpdateAsync(updatedTransactionDto);
            return NoContent();
        }

        // DELETE: api/transactions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {

            await _transactionService.RemoveAsync(id);
            return NoContent();
        }
    }
}
