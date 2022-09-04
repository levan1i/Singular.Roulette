using ge.singular.roulette;
using Microsoft.AspNetCore.Http;
using Singular.Roulette.Common.Exceptions;
using Singular.Roulette.Common.Extentions;
using Singular.Roulette.Domain.Interfaces;
using Singular.Roulette.Services.Abstractions;
using Singular.Roulette.Services.Abstractions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Services
{
    public class BetService : IBetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BetService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BetResult> MakeBet(BetDto betDto)
        {
            IsBetValidResponse ibvr = CheckBets.IsValid(betDto.JsonBetString);
            if (!ibvr.getIsValid())
            {
                throw new InvalidParametersException("Bet String Is Invalid", "invalid_bet");
            }

            var betamount = ibvr.getBetAmount();
            var userBallance =await _unitOfWork.Users.GetUserBallance(_httpContextAccessor.GetUserId(), "USD");
            var useraccount = await _unitOfWork.Users.GetUserAccount(_httpContextAccessor.GetUserId(),"USD");
            if (userBallance < betamount)
            {
                throw new InvalidParametersException("Insufficient funds", "insufficient_funds");
            }
            var bet =await  _unitOfWork.Bets.Add(new Domain.Models.Bet()
            {
                BetAmount = ibvr.getBetAmount(),
                UserIpAddress = _httpContextAccessor.GetUserIp(),
                BetStringJson = betDto.JsonBetString,
                isFinnished = false,
                UserId = _httpContextAccessor.GetUserId()
            });
            _unitOfWork.Complete();



            var random = 20;// RandomGenerator.Next(0, 36);
            int estWin = CheckBets.EstimateWin(betDto.JsonBetString, random);
            var spin =await _unitOfWork.Spins.Add(new Domain.Models.Spin()
            {
                CreateDate = DateTime.Now,
                WiningNumber = random
            });
             _unitOfWork.Complete();

            bet.isFinnished = true;
            bet.WonAmount = estWin;
            bet.SpinId = spin.SpinId;
            _unitOfWork.Bets.Update(bet);
            _unitOfWork.Complete();
            if (estWin == 0)
            {
              await  _unitOfWork.Transactions.MakeBetTransaction(useraccount.Id, betamount);
            }
            else
            {
                await _unitOfWork.Transactions.MakeBetWinTransaction(useraccount.Id, estWin);
            }



            return new BetResult(random, estWin);
        }

        public static class RandomGenerator
        {


            static RandomNumberGenerator csp = RandomNumberGenerator.Create();
            public static int Next(int minValue, int maxExclusiveValue)
            {
               
                if (minValue >= maxExclusiveValue)
                    throw new ArgumentOutOfRangeException("minValue must be lower than maxExclusiveValue");

                long diff = (long)maxExclusiveValue - minValue;
                long upperBound = uint.MaxValue / diff * diff;

                uint ui;
                do
                {
                    ui = GetRandomUInt();
                } while (ui >= upperBound);
                
                return (int)(minValue + (ui % diff));

            }

            private static uint GetRandomUInt()
            {
                var randomBytes = GenerateRandomBytes(sizeof(uint));
                return BitConverter.ToUInt32(randomBytes, 0);
            }

            private static byte[] GenerateRandomBytes(int bytesNumber)
            {
                byte[] buffer = new byte[bytesNumber];
                csp.GetBytes(buffer);
                return buffer;
            }
        }
    }
}
