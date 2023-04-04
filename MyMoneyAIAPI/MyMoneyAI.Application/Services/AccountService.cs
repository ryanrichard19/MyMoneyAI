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
    public class AccountService: BaseService<Account>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IGenericRepository<Account> repository, IUserContext userContext, IAccountRepository accountRepository)
         : base(repository, userContext)
        {
            _accountRepository = accountRepository;
        }

       
    }
}
