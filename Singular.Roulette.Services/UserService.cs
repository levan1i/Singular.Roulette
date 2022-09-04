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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> Create(UserDto user)
        {
          var res = await _unitOfWork.Users.Add(new Domain.Models.User()
            {
                CreateDate = DateTime.Now,
                Password =sha256_hash(user.Password),
                UserName = user.UserName,
            });
             _unitOfWork.Complete();


           await _unitOfWork.Users.CreateUserAccounts(res);
            _unitOfWork.Complete();
            user.UserId = res.UserId;
            user.Password = null;


        
          
            return user;
            
            
        }

        public async Task<UserDto> Get(string username)
        {
           var user=await _unitOfWork.Users.FindByUserName(username);
           if(user == null)
            {
                throw new DataNotFoundException("User Not Found");
            }

            return new UserDto
            {
                UserId=user.UserId,
                UserName=user.UserName
            };
        }

        public async Task<long> ValidatePassword(string UserName, string Password)
        {

             var user = await _unitOfWork.Users.FindByUserName(UserName);
            if (user == null) return 0;
       
         

            if (sha256_hash(Password) == user.Password) return user.UserId;
            return 0;
                  
        }

        private static string sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public async Task<UserDto> Get(long UserId)
        {
            var user = await _unitOfWork.Users.Get(UserId);
            if (user == null)
            {
                throw new DataNotFoundException("User Not Found");
            }

            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName
            };
        }

        public async Task<BallaceDto> GetBallance()
        {
            var ballance =await _unitOfWork.Users.GetUserBallance(httpContextAccessor.GetUserId(),"USD");
            if(!ballance.HasValue)
            {
                throw new DataNotFoundException("User Account Not Found");
            }

            return new BallaceDto(ballance.Value);
        }

        public async Task AddUserHeartBeet(string sessionId, long userid)
        {
            await _unitOfWork.HeartBeet.Add(new Domain.Models.HeartBeet()
            {
                LastUpdate = DateTime.Now,
                SessionId = sessionId,
                UserId = userid
            });
            _unitOfWork.Complete();
        }

        public async Task<DateTime> GetUserHeartBeet(string sessionId)
        {
            var heartbeet = await _unitOfWork.HeartBeet.Get(sessionId);
            return heartbeet.LastUpdate;
        }

        public async Task UpdateUserHeartBeet(string sessionId)
        {
            var heartbeet = await _unitOfWork.HeartBeet.Get(sessionId);
            heartbeet.LastUpdate = DateTime.Now;  
            _unitOfWork.HeartBeet.Update(heartbeet); ;
             _unitOfWork.Complete();

        }
    }
}
