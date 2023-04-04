using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Domain.Entities
{
    public class Transaction: BaseEntity
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
