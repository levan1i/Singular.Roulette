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
        /// Endpoint for Make Bet. In case of wrong bet string or Insufficient funds, returnes 400 bad request
        /// </summary>
        /// <remarks> 
        /// - Bet Json string example
        /// - "[{\\"T\\": \\"v\\", \\"I\\": 20, \\"C\\": 1, \\"K\\": 1}]"
        /// 
        /// </remarks>
        /// <param name="bet"></param>
        /// <returns></returns>
        [HttpPost("Make")]
       
        [ProducesResponseType(typeof(BetResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.InternalServerError)]
        public Task<BetResult> Make(BetDto bet) => _betService.MakeBet(bet);


        /// <summary>
        /// Endpoint for  current jacpot amount
        /// </summary>
        /// <remarks>
        /// Also It is Possible to use SignalR for continuously updated information
        /// - Hub endpoint  /BetHub
        /// - Hub requires authorization, use access_token to connect.
        /// </remarks>
        /// <returns></returns>
        [HttpGet("CalcJackpot")]
        [ProducesResponseType(typeof(BallaceDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.InternalServerError)]
        public Task<BallaceDto> CalcJackpot() => _betService.CalcJackpot();
    }
}
