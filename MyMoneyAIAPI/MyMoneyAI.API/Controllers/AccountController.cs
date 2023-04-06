using Microsoft.AspNetCore.Mvc;
using MyMoneyAI.Application.DTOs;
using MyMoneyAI.Application.Interfaces;
using MyMoneyAI.Application.Services;
using MyMoneyAI.Domain.Entities;
using MyMoneyAI.Domain.Interfaces;

namespace MyMoneyAI.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        // GET: api/accounts
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountService.ListAsync();
            return Ok(accounts);
        }

        // GET: api/accounts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var account = await _accountService.FindByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        // POST: api/accounts
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDto account)
        {
            await _accountService.AddAsync(account);
            return CreatedAtAction(nameof(GetTransactionById), new { id = account.Id }, account);
        }

        // PUT: api/accounts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountDto updatedAccount)
        {
            if (id != updatedAccount.Id)
            {
                return BadRequest();
            }

            await _accountService.UpdateAsync(updatedAccount);
            return NoContent();
        }

        // DELETE: api/accounts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {

            await _accountService.RemoveAsync(id);
            return NoContent();
        }

    }
}
