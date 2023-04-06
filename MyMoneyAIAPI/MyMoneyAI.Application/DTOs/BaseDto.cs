using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Application.DTOs
{
    public abstract record BaseDto
    {
        public int Id { get; init; }
    }
}
