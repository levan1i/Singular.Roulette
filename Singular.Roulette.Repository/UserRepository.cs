using Microsoft.EntityFrameworkCore;
using Singular.Roulette.Domain.Interfaces;
using Singular.Roulette.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Singular.Roulette.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SingularDbContext context) : base(context)
        {
        }

        public async Task<User> CreateUserAccounts(User user)
        {
            var createduser = user;
            var CreatedUserAccount=  _context.Accounts.Add(new Account
            {
                 Currency="USD",
                 TypeId = 1,
                UserId = createduser.UserId

            });
          
          

            return createduser;

        }

        public Task<User> FindByUserName(string username) => _context.Users.FirstOrDefaultAsync(x => x.UserName == username);

        public Task<Account> GetUserAccount(long UserId, string Currency) => _context.Accounts.FirstOrDefaultAsync(x => x.UserId == UserId && x.Currency == Currency&&x.TypeId==1);

        public async Task<Account?> GetUserBalance(long UserId, string currency)
        {
            var account =await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(x => x.TypeId == 1 &&x.Currency==currency&&x.UserId == UserId);
            if (account == null) return null;
            var plusblocks =  _context.Transactions.Where(x => x.ToAccountId == account.Id && x.TransactionStatusCode == 201).Sum(x => x.Amount);
            var minusblocks = _context.Transactions.Where(x => x.FromAccountId == account.Id && x.TransactionStatusCode == 201).Sum(x => x.Amount);
            account.Balance= account.Balance + plusblocks - minusblocks;
            return account;
        }

       
    }
}
