using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Singular.Roulette.Common.Exceptions;
using Singular.Roulette.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Common.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
       // private readonly Serilog.ILogger _log = Log.ForContext<ErrorHandlerMiddleware>();
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {


            try
            {
                await _next(context);
            }
            catch (Exception error)
            {

               // _log.Error(error, "");
                var response = context.Response;
                response.ContentType = "application/json";
                var resultdata = new ResultBase();
                switch (error)
                {
                    case DataNotFoundException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        resultdata.ErrorMessage = e.Message;
                        resultdata.ErrorCode = e.Code;

                        break;
                    case InvalidParametersException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        resultdata.ErrorMessage = e.Message;
                        resultdata.ErrorCode = e.Code;

                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        resultdata.ErrorMessage = error.Message;
                        break;
                }

                var result = JsonConvert.SerializeObject(resultdata);
                await response.WriteAsync(result);
            }
        }
    }
}
