using BirthdayChecker.Application.Services.ImageService;
using Infrastructure.Adapters.ImageService;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace BirthdayChecker.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        services.ConfigureOptions<BirthdayCheckerBackgroundJobSetup>();

        services.AddScoped<ImageServiceBase, CloudinaryImageServiceAdapter>();
        return services;
    }
}
