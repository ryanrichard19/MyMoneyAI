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
    public class TransactionService :ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Transaction>> ListAsync()
        {
            return await _transactionRepository.ListAsync();
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _transactionRepository.FindByIdAsync(id);
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _transactionRepository.AddAsync(transaction);
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            await _transactionRepository.Update(transaction);
        }

        public async Task RemoveAsync(int id)
        {
            var transaction = await _transactionRepository.FindByIdAsync(id);
            if (transaction != null)
            {
                await _transactionRepository.Remove(transaction);
            }
        }
    }
}
