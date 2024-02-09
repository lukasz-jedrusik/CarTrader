using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CarTrader.Services.Cars.Application.Commands.AddCar;
using CarTrader.Services.Cars.Application.Commands.RegisterCar;
using CarTrader.Services.Cars.Application.DataTransferObjects;
using CarTrader.Services.Cars.Domain.Models;
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
                async ([Required][FromBody]CarDto carDto, IMediator mediator, IMapper mapper) =>
                {
                    var car = mapper.Map<Car>(carDto);
                    var command = new AddCarCommand() { Car = car };
                    var result = await mediator.Send(command);
                    return Results.Created($"/cars/{result}", new { Id = result });
                });

            app.MapPost(
                "/cars/{carId}/register",
                async ([Required][FromRoute]string carId, IMediator mediator) =>
                {
                    var command = new RegisterCarCommand() { CarId = Guid.Parse(carId) };
                    await mediator.Send(command);
                    return Results.NoContent();
                });
        }
    }
}
