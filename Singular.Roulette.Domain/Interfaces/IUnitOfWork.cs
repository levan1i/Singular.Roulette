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
        IUserRepository Users { get; }
        int Complete();
    }
 
}
