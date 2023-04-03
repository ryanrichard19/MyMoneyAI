using MyMoneyAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> ListAsync();
        Task AddAsync(Account account);
        Task<Account> FindByIdAsync(int id);
        void Update(Account account);
        void Remove(Account account);
    }
}
