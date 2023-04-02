using MyMoneyAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> RegisterUserAsync(User user, string password);
        Task<User> GetUserByIdAsync(string userId);
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> CheckPasswordAsync(User user, string password);
    }
}
