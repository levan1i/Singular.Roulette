using Singular.Roulette.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindByUserName(string username);
        Task<User> CreateUserAccounts(User user);
        Task<Account> GetUserAccount(long UserId, string Currency);
        Task<Account?> GetUserBalance(long UserId, string currency);
       
    }


}
