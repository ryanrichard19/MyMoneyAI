using AutoMapper;
using MyMoneyAI.Application.DTOs;
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
    public class TransactionService : BaseService<Transaction, TransactionDto>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IGenericRepository<Transaction> repository, IUserContext userContext, ITransactionRepository transactionRepository, IMapper mapper)
       : base(repository, userContext, mapper)
        {
            _transactionRepository = transactionRepository;
        }

    }
}
