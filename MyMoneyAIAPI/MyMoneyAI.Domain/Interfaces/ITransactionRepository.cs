using MyMoneyAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> ListAsync();
        Task AddAsync(Transaction transaction);
        Task<Transaction> FindByIdAsync(int id);
        Task Update(Transaction transaction);
        Task Remove(Transaction transaction);
    }
}
