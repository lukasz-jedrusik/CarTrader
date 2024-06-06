using CarTrader.Services.ParkingPlaces.Application.Interfaces.Repositories;
using CarTrader.Services.ParkingPlaces.Application.Interfaces.Services;
using CarTrader.Services.ParkingPlaces.Application.Messages;
using CarTrader.Services.ParkingPlaces.Domain.Exceptions;
using CarTrader.Services.ParkingPlaces.Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CarTrader.Services.ParkingPlaces.Application.Commands.SetParkingPlace
{
    public class SetParkingPlaceCommandHandler : IRequestHandler<SetParkingPlaceCommand, CarParkingPlace>
    {
        private readonly ICarParkingPlaceRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMessagePublisher _messagePublisher;
        private readonly List<string> sectors;
        private readonly int maxSpacesPerSector;
        private readonly Random random = new();

        public SetParkingPlaceCommandHandler(
            ICarParkingPlaceRepository repository,
            IConfiguration configuration,
            IMessagePublisher messagePublisher
            )
        {
            _repository = repository;
            _configuration = configuration;
            _messagePublisher = messagePublisher;
            maxSpacesPerSector = _configuration.GetValue<int>("ParkingPlacesConfiguration:PlaceInSector");
            sectors = _configuration.GetSection("ParkingPlacesConfiguration:Sectors").Get<List<string>>();
        }

        public async Task<CarParkingPlace> Handle(SetParkingPlaceCommand request, CancellationToken cancellationToken)
        {
            // get occupated spots from db
            var carsSpots = await _repository.GetAllAsync();

            // initilize list of spots
            List<Spot> spots = [];

            // crete list of available spots
            foreach (string sector in sectors)
            {
                for (int space = 1; space <= maxSpacesPerSector; space++)
                {
                    if (!carsSpots.Any(x => x.Sector == sector && x.PlaceNumber == space))
                    {
                        spots.Add(new Spot(sector, space));
                    }
                }
            }

            // exception if no available spot
            if (spots.Count == 0)
            {
                throw new NoSpotException(request.CarId, request.BussinesKey);
            }

            // random available spot
            int randomIndex = random.Next(spots.Count);
            Spot randomSpot = spots[randomIndex];

            // create parkingPlace domain object
            var place = new CarParkingPlace()
            {
                CarId = request.CarId,
                BussinesKey = request.BussinesKey,
                Sector = randomSpot.Sector,
                PlaceNumber = randomSpot.PlaceNumber
            };

            // add spot to db
            if (!carsSpots.Any(x => x.CarId == request.CarId))
            {
                await _repository.AddAsync(place);
            }

            return place;

            // create message
            var message = new CompleteExternalTaskMessage(request.CarId, "External_Task_Set_Parking_Place");

            // publish message to RabbitMq
            await _messagePublisher.PublishMessageAsync("CarTrader.Cars", "CompleteExternalTask", message);
        }
    }
}