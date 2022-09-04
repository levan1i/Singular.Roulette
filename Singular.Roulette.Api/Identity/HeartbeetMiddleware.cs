using Singular.Roulette.Services.Abstractions;

namespace Singular.Roulette.Api.Identity
{

    public class HeartbeetMiddleware
    {
        private readonly RequestDelegate _next;

        // private readonly Serilog.ILogger _log = Log.ForContext<ErrorHandlerMiddleware>();
        public HeartbeetMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    IUserService _userService = context.RequestServices.GetService<IUserService>();
                    await _userService.UpdateUserHeartBeet(context.User.Claims.First(x => x.Type == "sessionId").Value);
                }
               
                
            }
            catch
            {

            }
         

           
            await _next(context);
        }
    }
}

