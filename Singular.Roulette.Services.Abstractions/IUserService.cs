using Singular.Roulette.Common.Types;
using Singular.Roulette.Services.Abstractions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Services.Abstractions
{
    public interface IUserService
    {
        /// <summary>
        /// Get By User Id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<UserDto> Get(long UserId);
        /// <summary>
        /// Get by user name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<UserDto> Get(string username);
        /// <summary>
        /// Create User with main game and jackpot accounts
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<UserDto> Create(UserDto user);
        /// <summary>
        /// Get user account with calculated ballance
        /// </summary>
        /// <returns></returns>
        Task<BallaceDto> GetBallance();
        /// <summary>
        /// Add user heart beet
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task AddUserHeartBeet(string sessionId, long userid);
        /// <summary>
        /// Get user last heart beet time
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        Task<DateTime> GetUserHeartBeet(string sessionId);
        /// <summary>
        /// Validate user entered password
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Task<long> ValidatePassword(string UserName, string Password);
        /// <summary>
        /// Update User last heart beet
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        Task UpdateUserHeartBeet(string sessionId);

        /// <summary>
        /// Get paged game history
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        Task<PagedResult<GameHistoryDto>> GetGameHistory(int page, int pagesize);

    }
}
