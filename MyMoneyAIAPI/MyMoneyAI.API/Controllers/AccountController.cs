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
            var accountsDtoList = await _accountService.ListAsync();
            return Ok(accountsDtoList);
        }

        // GET: api/accounts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var accountDto = await _accountService.FindByIdAsync(id);
            if (accountDto == null)
            {
                return NotFound();
            }
            return Ok(accountDto);
        }

        // POST: api/accounts
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountDto accountDto)
        {
            var newAccountDto = await _accountService.AddAsync(accountDto);
            return CreatedAtAction(nameof(GetAccountById), new { id = newAccountDto.Id }, newAccountDto);
        }

        // PUT: api/accounts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] AccountDto updatedAccountDto)
        {
            if (id != updatedAccountDto.Id)
            {
                return BadRequest();
            }

            await _accountService.UpdateAsync(updatedAccountDto);
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
