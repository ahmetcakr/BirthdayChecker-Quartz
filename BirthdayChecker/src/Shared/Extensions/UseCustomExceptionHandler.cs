using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Shared.Dtos;
using Shared.Exceptions;

namespace Shared.Extensions;

public static class UseCustomExceptionHandler
{
    public static void UseCustomException(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(config =>
        {
            config.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                var statusCode = exceptionFeature.Error switch
                {
                    ServerSideException serverSideException => serverSideException.StatusCode != 0 ? serverSideException.StatusCode : 400,
                    ClientNotFoundException => 404,
                    _ => 500
                };

                context.Response.StatusCode = statusCode;

                var response = ResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            });
        });
    }
}
