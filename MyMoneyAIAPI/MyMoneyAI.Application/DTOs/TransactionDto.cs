using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Application.DTOs
{
    public record TransactionDto : BaseDto
    {
        public string Description { get; init; }
        public decimal Amount { get; init; }
        public DateTime Date { get; init; }
        public int AccountId { get; init; }
    }
}
