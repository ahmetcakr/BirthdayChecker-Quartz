using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Dtos;

namespace Shared.Extensions;

public static class CustomValidationResponse
{
    public static void UseCustomValidationResponse(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0)
                    .SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                return new BadRequestObjectResult(ResponseDto<NoContentDto>.Fail(errors.ToList()));
            };
        });
    }
}
