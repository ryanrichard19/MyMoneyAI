using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
