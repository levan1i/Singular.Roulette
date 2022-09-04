using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Common.Extentions
{
    public static class HttpContexAccessorExtentions
    {
        /// <summary>
        /// Returnes UserID based on sub claim 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static long GetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            return Convert.ToInt64(httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "sub").Value);

        }

        //todo retrive ip from x-forwarder-for header
        /// <summary>
        /// Get user ip from htto request
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static string GetUserIp(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

        }
    }
}
