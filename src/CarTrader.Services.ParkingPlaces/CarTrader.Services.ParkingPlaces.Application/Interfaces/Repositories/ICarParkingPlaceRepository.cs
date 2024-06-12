using CarTrader.Services.ParkingPlaces.Domain.Models;

namespace CarTrader.Services.ParkingPlaces.Application.Interfaces.Repositories
{
    public interface ICarParkingPlaceRepository
    {
        Task AddAsync(CarParkingPlace item);
        Task<List<CarParkingPlace>> GetAllAsync();
        Task<CarParkingPlace> GetByIdAsync(Guid itemId);
    }
}