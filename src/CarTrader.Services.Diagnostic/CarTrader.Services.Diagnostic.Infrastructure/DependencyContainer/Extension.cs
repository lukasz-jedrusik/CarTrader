using Microsoft.Extensions.DependencyInjection;
using CarTrader.Services.Diagnostic.Application.Services;
using CarTrader.Services.Diagnostic.Application.Interfaces.Services;
using CarTrader.Services.Diagnostic.Infrastructure.Services;
using CarTrader.Services.Diagnostic.Application.Interfaces.Repositories;
using CarTrader.Services.Diagnostic.Infrastructure.Repositories;

namespace CarTrader.Services.Diagnostic.Infrastructure.DependencyContainer
{
    public static class Extension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ICarDiagnosticRepository, CarDiagnosticRepository>();

            // Services
            services.AddSingleton<IMessagePublisher, MessagePublisher>();
            services.AddSingleton<IMessageSubscriber, MessageSubscriber>();

            // Message Subscribers
            services.AddHostedService<RegisterCarinDiagnosticMsgSubscriberService>();

            // Queue
            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue>(_ =>
            {
                const int queueCapacity = 100;
                return new BackgroundTaskQueue(queueCapacity);
            });

            return services;
        }
    }
}