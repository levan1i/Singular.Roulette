using ge.singular.roulette;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Singular.Roulette.Common.Exceptions;
using Singular.Roulette.Common.Extentions;
using Singular.Roulette.Domain.Interfaces;
using Singular.Roulette.Services.Abstractions;
using Singular.Roulette.Services.Abstractions.Dtos;
using Singular.Roulette.Services.Hubs;
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
        IHubContext<BetHub> _bethub;

        public BetService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IHubContext<BetHub> bethub)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _bethub = bethub;
        }

        public async Task<BallaceDto> CalcJackpot()
        {
            var amount =await _unitOfWork.Accounts.CalcJackpotAmount("USD");
            return new BallaceDto(amount);
        }

        public async Task<BetResult> MakeBet(BetDto betDto)
        {
            IsBetValidResponse ibvr = CheckBets.IsValid(betDto.JsonBetString);
            if (!ibvr.getIsValid())
            {
                throw new InvalidParametersException("Bet String Is Invalid", "invalid_bet");
            }

            var betamount = ibvr.getBetAmount();
            var userAccountBallance =await _unitOfWork.Users.GetUserBallance(_httpContextAccessor.GetUserId(), "USD");
            //Check ballance against bet amount
            if (userAccountBallance.Ballance < betamount)
            {
                throw new InvalidParametersException("Insufficient funds", "insufficient_funds");
            }
            //Move funds from user main balance to user game and jackpot accounts
            //block that funds for future move on main accounts
            //and save bet record in 1 transaction
            var bet =  await _unitOfWork.Transactions.MakeBetTransaction(userAccountBallance.Id,userAccountBallance.Currency, betamount, new Domain.Models.Bet()
            {
                BetAmount = ibvr.getBetAmount(),
                UserIpAddress = _httpContextAccessor.GetUserIp(),
                BetStringJson = betDto.JsonBetString,
                isFinnished = false,
                UserId = _httpContextAccessor.GetUserId(),
                CreateDate = DateTime.Now

            });

            if (bet == null)
            {
                throw new Exception("An Error Has Occured");
            }


            //  var random = 17; 
            var random =  RandomGenerator.Next(0, 36);
            int estWin = CheckBets.EstimateWin(betDto.JsonBetString, random);
            //Save spin separately
            //Possible feature, multi user game
            var spin =await _unitOfWork.Spins.Add(new Domain.Models.Spin()
            {
                CreateDate = DateTime.Now,
                WiningNumber = random
            });
             _unitOfWork.Complete();

            bet.isFinnished = true;
            bet.WonAmount = estWin;
            bet.SpinId = spin.SpinId;

            //Update bet record with won amount and set spin Id
            _unitOfWork.Bets.Update(bet);
            _unitOfWork.Complete();
            if (estWin != 0)
            {
                //In Case of user win add funds to user account
                await _unitOfWork.Transactions.MakeBetWinTransaction(userAccountBallance.Id, estWin);
            }

            //Send Jackpot update to all connected users
            _ = _bethub.Clients.All.SendAsync("JackpotUpdate", await CalcJackpot());

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
