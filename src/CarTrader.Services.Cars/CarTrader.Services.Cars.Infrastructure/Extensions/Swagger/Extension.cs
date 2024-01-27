using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CarTrader.Services.Cars.Infrastructure.Extensions.Swagger
{
    public static class Extension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                        {
                            Title = "CarTrader.Services.Cars API",
                            Description = "ASP.NET Core CarTrader.Services.Cars API",
                            Contact = new OpenApiContact
                            {
                                Name = "Łukasz Jędrusik",
                                Email = "ljedrusik84@gmail.com",
                            }
                        });
            });

            return services;
        }
    }
}