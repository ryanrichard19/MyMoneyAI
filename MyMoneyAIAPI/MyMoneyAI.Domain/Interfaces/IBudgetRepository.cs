using MyMoneyAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Domain.Interfaces
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<Budget>> ListAsync();
        Task AddAsync(Budget budget);
        Task<Budget> FindByIdAsync(int id);
        Task Update(Budget budget);
        Task Remove(Budget budget);
    }
}
