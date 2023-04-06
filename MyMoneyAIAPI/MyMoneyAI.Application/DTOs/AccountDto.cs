using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Application.DTOs
{
    public record AccountDto : BaseDto
    { 
        public string Name { get; init; }
        public decimal Balance { get; init; }
    }
}
