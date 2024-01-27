using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarTrader.Services.Cars.Infrastructure.Extensions.EfCore
{
    public static class Extension
    {
        public static IServiceCollection AddEfCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(x =>
                x.UseSqlServer(configuration.GetConnectionString("CarTraderCarsDatabase")));

            return services;
        }
    }
}