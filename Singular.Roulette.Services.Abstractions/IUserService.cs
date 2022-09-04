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
        Task<UserDto> Get(long UserId);
        Task<UserDto> Get(string username);
        Task<UserDto> Create(UserDto user);
        Task<BallaceDto> GetBallance();
        Task AddUserHeartBeet(string sessionId, long userid);
        Task<DateTime> GetUserHeartBeet(string sessionId);
        Task<long> ValidatePassword(string UserName, string Password);
        Task UpdateUserHeartBeet(string sessionId);
    }
}
