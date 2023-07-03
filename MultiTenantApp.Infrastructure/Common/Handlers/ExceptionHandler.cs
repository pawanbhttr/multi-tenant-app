using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace MultiTenantApp.Infrastructure.Common.Handlers
{
    public static class ExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    if (contextFeatures != null)
                    {
                        var errorMessage = contextFeatures.Error.Message;

                        await context.Response.WriteAsync(
                            System.Text.Json.JsonSerializer.Serialize(
                            new
                            {
                                context.Response.StatusCode,
                                Message = errorMessage
                            }));
                    }
                });
            });
        }
    }
}
