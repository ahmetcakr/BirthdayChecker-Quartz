using BirthdayChecker.Application;
using BirthdayChecker.Infrastructure;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using BirthdayChecker.Persistence;
using Swashbuckle.AspNetCore.SwaggerUI;
using BirthdayChecker.WebAPI;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddApplicationServices();

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache(); // InMemory

// builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = "localhost:6379"); // Redis

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(
    opt =>
        opt.AddDefaultPolicy(p =>
        {
            p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        })
);
builder.Services.AddSwaggerGen(opt =>
{

});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.DocExpansion(DocExpansion.None);
    });
}

if (app.Environment.IsProduction())
    app.ConfigureCustomExceptionMiddleware();

app.MapControllers();

const string webApiConfigurationSection = "WebAPIConfiguration";
WebAPIConfiguration webApiConfiguration =
    app.Configuration.GetSection(webApiConfigurationSection).Get<WebAPIConfiguration>()
    ?? throw new InvalidOperationException($"\"{webApiConfigurationSection}\" section cannot found in configuration.");
app.UseCors(opt => opt.WithOrigins(webApiConfiguration.AllowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials());

app.Run();
