using Singular.Roulette.Common.Exceptions;
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

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            user.UserId = res.UserId;
          
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
    }
}
