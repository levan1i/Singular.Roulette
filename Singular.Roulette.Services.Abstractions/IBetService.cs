using Singular.Roulette.Services.Abstractions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Services.Abstractions
{
    public interface IBetService
    {
        /// <summary>
        /// Make Bet
        /// </summary>
        /// <param name="Currency"></param>
        /// <returns></returns>
        Task<BetResult> MakeBet(BetDto betDto);
        /// <summary>
        /// Calculate jackpot based on jackpot account and blocked funds
        /// </summary>
        /// <param name="Currency"></param>
        /// <returns></returns>
        Task<BallaceDto> CalcJackpot();
        

    }
}
