using System.ComponentModel.DataAnnotations;
using CarTrader.Services.Cars.Application.Commands.AddCar;
using CarTrader.Services.Cars.Application.DataTransferObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarTrader.Services.Cars.Api.Endpoints
{
    public static class CarsEndpoints
    {
        public static void AddCarsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost(
                "/cars",
                async ([Required][FromBody]CarDto carDto, IMediator mediator) =>
                {
                    var command = new AddCarCommand() { Car = carDto };
                    var result = await mediator.Send(command);
                    return Results.Created($"/cars/{result}", new { Id = result });
                });
        }
    }
}
