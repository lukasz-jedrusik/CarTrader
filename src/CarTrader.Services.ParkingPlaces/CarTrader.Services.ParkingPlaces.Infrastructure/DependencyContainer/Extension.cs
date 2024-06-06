using Microsoft.Extensions.DependencyInjection;
using CarTrader.Services.ParkingPlaces.Application.Services;
using CarTrader.Services.ParkingPlaces.Application.Interfaces.Services;
using CarTrader.Services.ParkingPlaces.Application.Interfaces.Repositories;
using CarTrader.Services.ParkingPlaces.Infrastructure.Repositories;
using CarTrader.Services.ParkingPlaces.Infrastructure.Services;
using CarTrader.Services.ParkingPlaces.Application.Handlers;
using CarTrader.Services.ParkingPlaces.Application.Interfaces.Handlers;
using CarTrader.Services.ParkingPlaces.Application.Messages;
using CarTrader.Services.ParkingPlaces.Application.Responses;
using CarTrader.Services.ParkingPlaces.Application.Requests;

namespace CarTrader.Services.ParkingPlaces.Infrastructure.DependencyContainer
{
    public static class Extension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ICarParkingPlaceRepository, CarParkingPlaceRepository>();

            // Services
            services.AddSingleton<IMessagePublisher, MessagePublisher>();
            services.AddSingleton<IMessageSubscriber, MessageSubscriber>();
            services.AddHostedService<MessagingBackgroundService>();

            // Handlers
            // services.AddSingleton<IMessageHandler<ParkingPlaceSetMessage>, SetParkingPlaceMessageHandler>();
            services.AddSingleton<
                IRequestResponseHandler<SetParkingPlaceRequest, SetParkingPlaceResponse>,
                SetParkingPlaceMessageHandler>();

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