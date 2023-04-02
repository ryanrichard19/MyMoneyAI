using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Domain.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Account> Accounts { get; set; }
        public ICollection<Budget> Budgets { get; set; }
    }
}
