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


        /// <summary>
        /// Get user ip from http request
        /// </summary>
        /// <remarks>
        /// In case of working with proxy or load balancer
        /// <c></c>
        /// Use forwarded headers option and register forwarded headers middleware
        /// 
        /// 
        /// </remarks>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static string GetUserIp(this IHttpContextAccessor httpContextAccessor)
        {

            return httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

        }
    }
}
