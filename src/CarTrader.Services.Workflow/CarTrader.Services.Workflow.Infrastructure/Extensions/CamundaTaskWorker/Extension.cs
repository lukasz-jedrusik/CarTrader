using System.Text;
using Camunda.Worker;
using Camunda.Worker.Client;
using CarTrader.Services.Workflow.Infrastructure.Handlers.CaumundaHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarTrader.Services.Workflow.Infrastructure.Extensions.CamundaTaskWorker
{
    public static class Extension
    {
        public static IServiceCollection AddCamundaTaskWorker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExternalTaskClient(
                client => {
                    string credentials = configuration["Camunda:Username"] + ":" + configuration["Camunda:Password"];
                    var byteArray = Encoding.ASCII.GetBytes(credentials);
                    client.BaseAddress = new Uri(configuration["Camunda:ServerUrl"]);
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(byteArray));
                }
            );

            services.AddCamundaWorker("CarTraderWorker")
                .AddHandler<SetParkingPlaceHandler>();

            return services;
        }
    }
}