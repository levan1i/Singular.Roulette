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
        Task<long> ValidatePassword(string UserName, string Password);
    }
}
