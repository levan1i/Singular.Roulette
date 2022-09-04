using Singular.Roulette.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Domain.Interfaces
{
    public interface ITransactionRepository :IGenericRepository<Transaction>
    {
        Task<Bet> MakeBetTransaction(long AccountId,decimal Amount,Bet bet);
        Task<bool> MakeBetWinTransaction(long AccountId, decimal Amount);
        Task<IEnumerable<Transaction>> GetBlockedTransactions();
        Task<bool> FinishBlockedFundsTransaction(long transactionId);
    }
}
