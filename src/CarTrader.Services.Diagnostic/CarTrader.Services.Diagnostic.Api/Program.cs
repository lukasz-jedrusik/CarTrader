using System.Text.Json.Serialization;
using CarTrader.Services.Diagnostic.Api.Middleware;
using CarTrader.Services.Diagnostic.Application.Mappings;
using CarTrader.Services.Diagnostic.Infrastructure.DependencyContainer;
using CarTrader.Services.Diagnostic.Infrastructure.Extensions.EfCore;
using CarTrader.Services.Diagnostic.Infrastructure.Extensions.KeycloakAuth;
using CarTrader.Services.Diagnostic.Infrastructure.Extensions.MediatR;
using CarTrader.Services.Diagnostic.Infrastructure.Extensions.RabbitMq;
using CarTrader.Services.Diagnostic.Infrastructure.Extensions.Swagger;
using Microsoft.AspNetCore.Http.Json;
using NLog.Web;

// Create builder
var builder = WebApplication.CreateBuilder(args);

// Add controllers to services
builder.Services
    .AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true);

// Specify json options
builder.Services.Configure<JsonOptions>(options =>
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
    .AddMediatR()
    .AddRabbitMq(builder.Configuration)
    .AddApplication();

// Add healtchecks endpoints
builder.Services.AddHealthChecks();

// Create app
var app = builder.Build();

// Configure the HTTP request pipeline.
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

// Run app
await app.RunAsync();