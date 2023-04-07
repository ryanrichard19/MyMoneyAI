using Microsoft.AspNetCore.Identity;

namespace MyMoneyAI.Domain.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Account> Accounts { get; set; }
        public ICollection<Budget> Budgets { get; set; }
        public ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    }
}
