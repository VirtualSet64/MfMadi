﻿using Microsoft.AspNetCore.Diagnostics;
using Sentry;
using System.Net;

namespace MfMadi.Common
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
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
                        SentrySdk.CaptureException(contextFeature.Error);
                    }
                });
            });
        }
    }
}
