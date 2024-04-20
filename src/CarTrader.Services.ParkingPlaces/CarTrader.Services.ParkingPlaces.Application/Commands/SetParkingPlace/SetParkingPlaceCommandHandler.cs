using CarTrader.Services.ParkingPlaces.Application.Interfaces.Repositories;
using CarTrader.Services.ParkingPlaces.Domain.Exceptions;
using CarTrader.Services.ParkingPlaces.Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CarTrader.Services.ParkingPlaces.Application.Commands.SetParkingPlace
{
    public class SetParkingPlaceCommandHandler : IRequestHandler<SetParkingPlaceCommand>
    {
        private readonly ICarParkingPlaceRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly List<string> sectors;
        private readonly int maxSpacesPerSector;
        private readonly Random random = new();

        public SetParkingPlaceCommandHandler(
            ICarParkingPlaceRepository repository,
            IConfiguration configuration
            )
        {
            _repository = repository;
            _configuration = configuration;
            maxSpacesPerSector = _configuration.GetValue<int>("ParkingPlacesConfiguration:PlaceInSector");
            sectors = _configuration.GetSection("ParkingPlacesConfiguration:Sectors").Get<List<string>>();
        }

        public async Task Handle(SetParkingPlaceCommand request, CancellationToken cancellationToken)
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
                    if (carsSpots.Any(x => x.Sector == sector && x.PlaceNumber == space))
                    {
                        spots.Add(new Spot(sector, space));
                    }
                }
            }

            // exception if no available sport
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
            await _repository.AddAsync(place);
        }
    }
}