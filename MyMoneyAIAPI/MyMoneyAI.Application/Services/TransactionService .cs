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
    public class TransactionService : BaseService<Transaction>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IGenericRepository<Transaction> repository, IUserContext userContext, ITransactionRepository transactionRepository)
       : base(repository, userContext)
        {
            _transactionRepository = transactionRepository;
        }

    }
}
