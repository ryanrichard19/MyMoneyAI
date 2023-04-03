using MyMoneyAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Application.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> ListAsync();
        Task<Account> GetByIdAsync(int accountId);
        Task AddAsync(Account account);
        Task UpdateAsync(Account account);
        Task RemoveAsync(int accountId);
    }
}
