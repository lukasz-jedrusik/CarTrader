using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace CarTrader.Services.ParkingPlaces.Infrastructure.Extensions.Swagger
{
    public static class Extension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CarTrader.Services.ParkingPlaces API",
                    Description = "ASP.NET Core CarTrader.Services.ParkingPlaces API",
                    Contact = new OpenApiContact
                    {
                        Name = "CANPACK SDD",
                        Email = "sdd-all@canpack.com",
                    }
                });
            });

            return services;
        }
    }
}