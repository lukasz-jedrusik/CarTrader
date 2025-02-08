using Microsoft.Extensions.DependencyInjection;
using CarTrader.Services.Diagnostic.Application.Services;
using CarTrader.Services.Diagnostic.Application.Interfaces.Services;

namespace CarTrader.Services.Diagnostic.Infrastructure.DependencyContainer
{
    public static class Extension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Repositories

            // Services

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