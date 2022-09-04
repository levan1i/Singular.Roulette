using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singular.Roulette.Common.Models;
using Singular.Roulette.Services.Abstractions;
using Singular.Roulette.Services.Abstractions.Dtos;
using System.Net;

namespace Singular.Roulette.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BetController : ControllerBase
    {
        private readonly IBetService _betService;

        public BetController(IBetService betService)
        {
            _betService = betService;
        }


        /// <summary>
        /// Make Bet, In case of wrong bet string or Insufficient funds, returnes 400 bad request
        /// </summary>
        /// <param name="bet"></param>
        /// <returns></returns>
        [HttpPost("Make")]
       
        [ProducesResponseType(typeof(BetResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.InternalServerError)]
        public Task<BetResult> Make(BetDto bet) => _betService.MakeBet(bet);


        /// <summary>
        /// Calculate Jackpot
        /// </summary>
        /// <returns></returns>
        [HttpGet("CalcJackpot")]
        [ProducesResponseType(typeof(BallaceDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.InternalServerError)]
        public Task<BallaceDto> CalcJackpot() => _betService.CalcJackpot();
    }
}
