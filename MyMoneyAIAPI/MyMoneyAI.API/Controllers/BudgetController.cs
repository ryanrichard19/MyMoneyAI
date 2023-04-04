using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMoneyAI.Application.Interfaces;

namespace MyMoneyAI.API.Controllers
{
    [Route("api/budgets")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }
    }
}
