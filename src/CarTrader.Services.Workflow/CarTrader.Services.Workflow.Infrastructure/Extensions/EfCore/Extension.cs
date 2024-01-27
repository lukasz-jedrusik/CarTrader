using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarTrader.Services.Workflow.Infrastructure.Extensions.EfCore
{
    public static class Extension
    {
        public static IServiceCollection AddEfCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(x =>
                x.UseSqlServer(configuration["CPCLEANAPI_CONNECTION_STRING"]))
                ;

            return services;
        }
    }
}