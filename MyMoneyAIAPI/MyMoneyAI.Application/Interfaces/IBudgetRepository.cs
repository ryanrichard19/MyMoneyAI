using MyMoneyAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Application.Interfaces
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<Budget>> ListAsync();
        Task AddAsync(Budget budget);
        Task<Budget> FindByIdAsync(int id);
        void Update(Budget budget);
        void Remove(Budget budget);
    }
}
