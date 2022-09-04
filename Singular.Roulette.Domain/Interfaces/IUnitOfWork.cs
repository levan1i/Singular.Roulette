using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    { 

        IBetRepository Bets { get; }
        ISpinRepository Spins { get; } 
        ITransactionRepository Transactions { get;  }
        IHeartBeetRepository HeartBeet { get; }
        IUserRepository Users { get; }
        IAccountRepository Accounts { get;  }
        int Complete();
    }
 
}
