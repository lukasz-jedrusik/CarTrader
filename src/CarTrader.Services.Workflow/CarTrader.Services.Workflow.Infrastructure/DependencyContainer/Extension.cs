using Microsoft.Extensions.DependencyInjection;
using CarTrader.Services.Workflow.Application.Services;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using CarTrader.Services.Workflow.Infrastructure.Services;
using Polly;
using Microsoft.Extensions.Configuration;

namespace CarTrader.Services.Workflow.Infrastructure.DependencyContainer
{
    public static class Extension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // Repositories

            // Services
            services.AddSingleton<IMessagePublisher, MessagePublisher>();
            services.AddSingleton<IMessageSubscriber, MessageSubscriber>();
            services.AddSingleton<ICamundaService, CamundaService>();
            services.AddHostedService<MessagingBackgroundService>();

            // HttpClients
            var retryPolicy = Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode).RetryAsync(3);
            services.AddHttpClient(
                "camundaHttpClient",
                client => client.BaseAddress = new Uri(configuration["Camunda:ServerUrl"]))
                .AddPolicyHandler(retryPolicy);

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