using Singular.Roulette.Services.Abstractions;
using System.IdentityModel.Tokens.Jwt;

namespace Singular.Roulette.Api.Identity
{
    public class TokenSessionMiddleware
    {
        private readonly RequestDelegate _next;

        // private readonly Serilog.ILogger _log = Log.ForContext<ErrorHandlerMiddleware>();
        public TokenSessionMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            IUserService _userService = context.RequestServices.GetService<IUserService>();

            if (context.Request.Headers.ContainsKey("Authorization"))
            {

                try
                {


                    var authorization = context.Request.Headers["Authorization"].SingleOrDefault();

                    var token = authorization.Substring(authorization.IndexOf(' ') + 1);
                    if (!authorization.StartsWith("Basic"))
                    {
                        var jwt = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
                        var sessionId = jwt.Claims.FirstOrDefault(x => x.Type == "sessionId")?.Value;
                        if (sessionId != null)
                        {
                            var heartbeet = await _userService.GetUserHeartBeet(sessionId);
                            if (heartbeet < DateTime.Now.AddMinutes(-5))
                            {
                                context.Request.Headers.Authorization = string.Empty;
                            }
                        }
                    }




                }
                catch (Exception ex)
            {
                context.Request.Headers.Authorization = string.Empty;
            }

        }
            await _next(context);
        }
    }
}
