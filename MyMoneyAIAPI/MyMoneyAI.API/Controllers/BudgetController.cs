using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMoneyAI.Application.DTOs;
using MyMoneyAI.Application.Interfaces;

namespace MyMoneyAI.API.Controllers
{
    [Route("api/budgets")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        // GET: api/budgets
        [HttpGet]
        public async Task<IActionResult> GetAllBudgets()
        {
            var budgets = await _budgetService.ListAsync();
            return Ok(budgets);
        }

        [HttpGet]
        [Route("budgets/{id}")]
        public async Task<IActionResult> GetBudgetById(int id)
        {
            var budget = await _budgetService.FindByIdAsync(id);
            if (budget == null)
            {
                return NotFound();
            }
            return Ok(budget);
        }

        // POST: api/budgets
        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] BudgetDto budgetDto)
        {
            var newBudgetDto = await _budgetService.AddAsync(budgetDto);
            return CreatedAtAction(nameof(GetBudgetById), new { id = newBudgetDto.Id }, newBudgetDto);
        }

        // PUT: api/budgets/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(int id, [FromBody] BudgetDto updatedBudgetDto)
        {
            if (id != updatedBudgetDto.Id)
            {
                return BadRequest();
            }
            await _budgetService.UpdateAsync(updatedBudgetDto);
            return NoContent();
        }

        // DELETE: api/budgets/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            var budget = await _budgetService.FindByIdAsync(id);
            if (budget == null)
            {
                return NotFound();
            }
            await _budgetService.RemoveAsync(id);
            return NoContent();
        }
    }
}
