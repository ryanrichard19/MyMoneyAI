using MyMoneyAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Application.Interfaces
{
    public interface IBudgetService
    {
        Task<IEnumerable<Budget>> ListAsync();
        Task<Budget> GetByIdAsync(int budgetId);
        Task AddAsync(Budget budget);
        Task UpdateAsync(Budget budget);
        Task RemoveAsync(int budgetId);
    }
}
