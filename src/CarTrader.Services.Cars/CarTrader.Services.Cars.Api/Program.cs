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

// Create builder
var builder = WebApplication.CreateBuilder(args);

// Add controllers to services
builder.Services
    .AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true);

// Specify json options
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );

// Add Nlog
builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Host.UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = false });

// Add services
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
    .AddApplication();

// Add healtchecks endpoints
builder.Services.AddHealthChecks();

// Create app
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use Cors, Middleware, https redirection, authorization in pipieline
app.UseCors()
    .UseMiddleware<ErrorHandlerMiddleware>()
    .UseHttpsRedirection()
    .UseAuthorization();

// Use controllers in pipieline
app.MapControllers();

// Use auto migrations applying
app.ApplyMigrations();

// Use healtheckecks in pipeline
app.MapHealthChecks("/health");

// Use app endpoints    
app.AddCarsEndpoints();

// Run app
await app.RunAsync();