using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Singular.Roulette.Common.Models;
using Singular.Roulette.Common.Types;
using Singular.Roulette.Services.Abstractions;
using Singular.Roulette.Services.Abstractions.Dtos;
using System.Net;

namespace Singular.Roulette.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class User : ControllerBase
    {
        private readonly IUserService _userService;

        public User(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Endpoint For User Registration
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.InternalServerError)]
        public Task<UserDto> Post(UserDto user) => _userService.Create(user);


        /// <summary>
        /// Endpoint For User Available balance calculation
        /// </summary>
        /// <returns></returns>
        [HttpGet("Balance")]
        [ProducesResponseType(typeof(BallaceDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.InternalServerError)]
        public Task<BallaceDto> Balance() => _userService.GetBalance();


        /// <summary>
        /// Endpoint for user game history. 
        /// </summary>
        /// <remarks>
        /// - Note: Endpoint has pagination
        /// - Default returns 10 records
        /// </remarks>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GameHistory")]
        [ProducesResponseType(typeof(PagedResult<GameHistoryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.InternalServerError)]
        public Task<PagedResult<GameHistoryDto>> GameHistory(int page=1,int pageSize=10) => _userService.GetGameHistory(page,pageSize);


        //To Do remove after development
        [HttpGet("Test")]        
        public int Test() => 1;
    }
}
