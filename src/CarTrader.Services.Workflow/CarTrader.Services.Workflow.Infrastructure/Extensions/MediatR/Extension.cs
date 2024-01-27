using System.Reflection;
using CarTrader.Services.Workflow.Application.Commands.StartProcess;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CarTrader.Services.Workflow.Infrastructure.Extensions.MediatR
{
    public static class Extension
    {
        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(StartProcessCommand).Assembly));
            return services;
        }
    }
}