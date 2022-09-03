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
        Task<BetResult> MakeBet(BetDto betDto);
    }
}
