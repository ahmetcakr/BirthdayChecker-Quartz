using BirthdayChecker.Application.Services.Users;
using Quartz;

namespace BirthdayChecker.Infrastructure;

[DisallowConcurrentExecution]
public class BirthdayCheckerBackgroundJob : IJob
{
    private readonly IUsersService _usersService;

    public BirthdayCheckerBackgroundJob(IUsersService usersService)
    {
        _usersService = usersService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var users = await _usersService.GetListAsync();

        if (users is null)
            return;

        var birthdayUsers = users?.Items.Where(u => u.BirthDate.Date == DateTime.Now.Date).ToList();

        if (birthdayUsers is null || birthdayUsers.Count == 0)
            return;

        birthdayUsers.ForEach(u => Console.WriteLine($"Happy birthday, {u.Name}!"));
    }
}
