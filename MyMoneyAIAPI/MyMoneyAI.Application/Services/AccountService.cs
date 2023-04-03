using MyMoneyAI.Application.Interfaces;
using MyMoneyAI.Domain.Entities;
using MyMoneyAI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Application.Services
{
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<Account>> ListAsync()
        {
            return await _accountRepository.ListAsync();
        }

        public async Task<Account> GetByIdAsync(int accountId)
        {
            return await _accountRepository.FindByIdAsync(accountId);
        }

        public async Task AddAsync(Account account)
        {
            await _accountRepository.AddAsync(account);
        }

        public async Task UpdateAsync(Account account)
        {
            await _accountRepository.Update(account);
        }

        public async Task RemoveAsync(int accountId)
        {
            var account = await _accountRepository.FindByIdAsync(accountId);
            if (account != null)
            {
                await _accountRepository.Remove(account);
            }
        }
    }
}
