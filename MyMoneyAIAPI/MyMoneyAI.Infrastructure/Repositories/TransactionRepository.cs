﻿using Microsoft.EntityFrameworkCore;
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
    public class TransactionRepository: ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> ListAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<Transaction> FindByIdAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task Update(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
