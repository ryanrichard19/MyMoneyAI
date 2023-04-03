using Microsoft.EntityFrameworkCore;
using MyMoneyAI.Domain.Entities;
using MyMoneyAI.Domain.Interfaces;
using MyMoneyAI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Infrastructure.Repositories
{
    public class BudgetRepository: IBudgetRepository
    {
        private readonly ApplicationDbContext _context;

        public BudgetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Budget>> ListAsync()
        {
            return await _context.Budgets.ToListAsync();
        }

        public async Task AddAsync(Budget budget)
        {
            await _context.Budgets.AddAsync(budget);
            await _context.SaveChangesAsync();
        }

        public async Task<Budget> FindByIdAsync(int id)
        {
            return await _context.Budgets.FindAsync(id);
        }

        public async Task Update(Budget budget)
        {
            _context.Budgets.Update(budget);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Budget budget)
        {
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
        }
    }
}
