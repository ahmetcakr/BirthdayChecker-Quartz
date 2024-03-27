using BirthdayChecker.Application.Services.Repositories;
using BirthdayChecker.Persistence.Contexts;
using BirthdayChecker.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BirthdayChecker.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration["ConnectionStrings:BaseDb"]));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
