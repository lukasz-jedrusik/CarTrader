using Microsoft.Extensions.DependencyInjection;
using CarTrader.Services.Notifications.Application.Services;
using CarTrader.Services.Notifications.Application.Interfaces.Services;

namespace CarTrader.Services.Notifications.Infrastructure.DependencyContainer
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