using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Cryptocop.Software.API.Models;
using Cryptocop.Software.API.Models.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Cryptocop.Software.API.Middleware
{
    public static class ExceptionHandlerExtensions
    {

        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            // TODO: Implement
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var exceptionHandlerfeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionHandlerfeature.Error;
                    var StatusCode = (int)HttpStatusCode.InternalServerError;

                    if (exception is ResourceNotFoundException)
                    {
                        StatusCode = (int)HttpStatusCode.NotFound;
                    }

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCode;

                    /* create exception model */
                    var new_model = new ExceptionModel();
                    new_model.StatusCode = StatusCode;
                    new_model.ExceptionMessage = exception.Message;
                    new_model.StackTrace = exception.StackTrace;


                    /* print to console */
                    await context.Response.WriteAsync(new_model.ToString());

                });
            });


        }
    }
}