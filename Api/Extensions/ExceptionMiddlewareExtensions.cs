using BusinessLayer.CrossCuttingConcerns.Logging;
using Entities.ErrorModels;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Api.Extensions;

public static class ExceptionMiddlewareExtensions 
{ 
    public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger) 
    {
        app.UseExceptionHandler(appError => 
        {
            appError.Run(async context => 
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    logger.LogError($"Something went wrong: {contextFeature.Error}");
                    //await context.Response.WriteAsync(
                    //    new ErrorDetails() 
                    //    { 
                    //        StatusCode = context.Response.StatusCode,
                    //        Message = "Internal Server Error.",
                    //    }.ToString());

                    // Üstteki blok front-end'de 500 Internal Error mesajı gönderirken hatanın detayını logluyor.
                    // CodeMaze'in kitabını bitirmediğim için önce alttakiyle hataları görmeliyim.
                    await context.Response.WriteAsync(
                        new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.ToString(),
                        }.ToString());
                }
            });
        });
    }
}
