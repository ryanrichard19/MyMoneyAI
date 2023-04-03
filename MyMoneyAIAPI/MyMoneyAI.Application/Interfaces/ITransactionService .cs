using MyMoneyAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> ListAsync();
        Task<Transaction> GetByIdAsync(int id);
        Task AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task RemoveAsync(int id);
    }
}
