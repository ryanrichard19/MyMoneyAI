using MyMoneyAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> ListAsync();
        Task AddAsync(Account account);
        Task<Account> FindByIdAsync(int id);
        Task Update(Account account);
        Task Remove(Account account);
    }
}
