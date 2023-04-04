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
    public class BudgetService: BaseService<Budget>, IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;


        public BudgetService(IGenericRepository<Budget> repository, IUserContext userContext, IBudgetRepository budgetRepository)
       : base(repository, userContext)
        {
            _budgetRepository = budgetRepository;
        }

       

    }
}
