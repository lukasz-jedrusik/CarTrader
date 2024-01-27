using System.Text.Json.Serialization;
using CarTrader.Services.Cars.Api.Endpoints;
using CarTrader.Services.Cars.Api.Middleware;
using CarTrader.Services.Cars.Application.Mappings;
using CarTrader.Services.Cars.Infrastructure.DependencyContainer;
using CarTrader.Services.Cars.Infrastructure.Extensions.EfCore;
using CarTrader.Services.Cars.Infrastructure.Extensions.FluentValidation;
using CarTrader.Services.Cars.Infrastructure.Extensions.KeycloakAuth;
using CarTrader.Services.Cars.Infrastructure.Extensions.MediatR;
using CarTrader.Services.Cars.Infrastructure.Extensions.RabbitMq;
using CarTrader.Services.Cars.Infrastructure.Extensions.Swagger;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    builder.Services
        .AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true);

    builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter())
        );

    builder.Logging.ClearProviders();
    builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
    builder.Logging.AddConsole();
    builder.Host.UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = false });

    builder.Services
        .AddCors()
        .AddAutoMapper(typeof(AutoMapperProfiles))
        .AddEfCore(builder.Configuration)
        .AddEndpointsApiExplorer()
        .AddSwagger()
        .AddKeycloakAuthorization(builder.Configuration)
        .AddFluentValidation()
        .AddMediatR()
        .AddRabbitMq(builder.Configuration)
        .AddApplication()
        ;

    builder.Services.AddHealthChecks();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors()
        .UseMiddleware<ErrorHandlerMiddleware>()
        .UseHttpsRedirection()
        .UseAuthorization();

    app.MapControllers();

    app.MapHealthChecks("/health");

    app.AddCarsEndpoints();

    app.Run();
}