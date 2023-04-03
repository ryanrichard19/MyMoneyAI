using MyMoneyAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Application.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> ListAsync();
        Task AddAsync(Transaction transaction);
        Task<Transaction> FindByIdAsync(int id);
        void Update(Transaction transaction);
        void Remove(Transaction transaction);
    }
}
