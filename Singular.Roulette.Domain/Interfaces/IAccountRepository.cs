using Singular.Roulette.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Domain.Interfaces
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        /// <summary>
        /// Calculate jackpot based on jackpot account and blocked funds
        /// </summary>
        /// <param name="Currency"></param>
        /// <returns></returns>
        Task<decimal> CalcJackpotAmount(string Currency);
    }
}
