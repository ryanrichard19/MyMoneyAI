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
    public class BudgetService: IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<IEnumerable<Budget>> ListAsync()
        {
            return await _budgetRepository.ListAsync();
        }

        public async Task<Budget> GetByIdAsync(int budgetId)
        {
            return await _budgetRepository.FindByIdAsync(budgetId);
        }

        public async Task AddAsync(Budget budget)
        {
            await _budgetRepository.AddAsync(budget);
        }

        public async Task UpdateAsync(Budget budget)
        {
            await _budgetRepository.Update(budget);
        }

        public async Task RemoveAsync(int budgetId)
        {
            var budget = await _budgetRepository.FindByIdAsync(budgetId);
            if (budget != null)
            {
                await _budgetRepository.Remove(budget);
            }
        }

    }
}
