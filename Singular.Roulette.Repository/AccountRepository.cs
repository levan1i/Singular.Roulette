using Microsoft.EntityFrameworkCore;
using Singular.Roulette.Domain.Interfaces;
using Singular.Roulette.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Repository
{
    internal class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(SingularDbContext context) : base(context)
        {
        }
      
        public async Task<decimal> CalcJackpotAmount(string Currency)
        {
            //Main Jackpot Account
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.TypeId == 101 && x.Currency == Currency);   
            
            //Blocked Funds on Account
            var plusblocks = _context.Transactions.Where(x => x.ToAccountId == account.Id && x.TransactionStatusCode == 201).Sum(x => x.Amount);
            var minusblocks = _context.Transactions.Where(x => x.FromAccountId == account.Id && x.TransactionStatusCode == 201).Sum(x => x.Amount);
            
            return account.Balance + plusblocks - minusblocks;
        }
    }
}
