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

        [HttpPost("Register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.InternalServerError)]
        public Task<UserDto> Post(UserDto user) => _userService.Create(user);

        [HttpGet("Ballance")]
        [ProducesResponseType(typeof(BallaceDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.InternalServerError)]
        public Task<BallaceDto> Ballance() => _userService.GetBallance();



        [HttpGet("GameHistory")]
        [ProducesResponseType(typeof(PagedResult<GameHistoryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResultBase), (int)HttpStatusCode.InternalServerError)]
        public Task<PagedResult<GameHistoryDto>> GameHistory(int page=1,int pageSize=10) => _userService.GetGameHistory(page,pageSize);

        [HttpGet("Ballance1")]
        
        public int Ballance1() => 1;
    }
}
