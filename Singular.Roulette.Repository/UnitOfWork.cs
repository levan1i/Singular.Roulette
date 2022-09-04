using Singular.Roulette.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IBetRepository bets, ISpinRepository spins, ITransactionRepository transactions, IUserRepository users, IHeartBeetRepository heartBeet, SingularDbContext context, IAccountRepository accounts)
        {
            Bets = bets;
            Spins = spins;
            Transactions = transactions;
            Users = users;
            HeartBeet = heartBeet;
            _context = context;
            Accounts = accounts;
        }
        private readonly SingularDbContext _context;
        public IBetRepository Bets { get; }
        public ISpinRepository Spins { get; }
        public ITransactionRepository Transactions { get; }
        public IUserRepository Users { get; }

        public IHeartBeetRepository HeartBeet { get; }
        public IAccountRepository Accounts { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
