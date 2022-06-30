using Application.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var erroResult = new ErrorDetails();
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        erroResult.Message = string.IsNullOrEmpty(contextFeature.Error?.Message) ? "Erro ocorrido, mensagem não capturada." : contextFeature.Error.Message;
                        

                        if (contextFeature.Error is ForbiddenAccessException e)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        }

                        if(contextFeature.Error is ValidationException validator)
                        {
                            erroResult.Message = "";
                            erroResult.Errors = validator.Errors;
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }


                        if (contextFeature.Error.InnerException != null)
                        {
                            erroResult.Message += $"<br/><br/>{contextFeature.Error.InnerException.Message}";
                        }


                        await context.Response.WriteAsync(erroResult.ToString());
                    }
                });
            });
        }
    }

    public class ErrorDetails
    {
        public string Message { get; set; }

        public IDictionary<string, string[]> Errors { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
