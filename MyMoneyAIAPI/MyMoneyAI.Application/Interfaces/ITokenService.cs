﻿using MyMoneyAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Application.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(User user);
    }
}
