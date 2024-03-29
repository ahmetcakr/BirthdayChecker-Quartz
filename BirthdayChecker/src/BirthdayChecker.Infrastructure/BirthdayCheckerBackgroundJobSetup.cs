using Microsoft.Extensions.Options;
using Quartz;

namespace BirthdayChecker.Infrastructure;

public class BirthdayCheckerBackgroundJobSetup : IConfigureOptions<QuartzOptions>
{

    public void Configure(QuartzOptions options)
    {
        JobKey jobKey = JobKey.Create(nameof(BirthdayCheckerBackgroundJob));

        options
            .AddJob<BirthdayCheckerBackgroundJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(options =>
                {
                    options
                    .ForJob(jobKey)
                    .WithSimpleSchedule(schedule => schedule.WithIntervalInHours(24)
                    .RepeatForever());
                });
    }
}
