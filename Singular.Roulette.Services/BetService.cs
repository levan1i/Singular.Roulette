using ge.singular.roulette;
using Singular.Roulette.Common.Exceptions;
using Singular.Roulette.Domain.Interfaces;
using Singular.Roulette.Services.Abstractions;
using Singular.Roulette.Services.Abstractions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Services
{
    public class BetService : IBetService
    {
        private readonly IBetRepository _betRepository;
        private readonly ISpinRepository _spinRepository;

        public BetService(IBetRepository betRepository, ISpinRepository spinRepository)
        {
            _betRepository = betRepository;
            _spinRepository = spinRepository;
        }

        public Task<BetResult> MakeBet(BetDto betDto)
        {
            IsBetValidResponse ibvr = CheckBets.IsValid(betDto.JsonBetString);
            if (!ibvr.getIsValid())
            {
                throw new InvalidParametersException("Bet String Is Invalid", "invalid_bet");
            }
            throw new NotImplementedException();
        }
    }
}
