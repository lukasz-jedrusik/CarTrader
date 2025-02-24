using CarTrader.Services.ParkingPlaces.Application.Interfaces.Repositories;
using CarTrader.Services.ParkingPlaces.Domain.Models;
using CarTrader.Services.ParkingPlaces.Infrastructure.Extensions.EfCore;
using Microsoft.EntityFrameworkCore;

namespace CarTrader.Services.ParkingPlaces.Infrastructure.Repositories
{
    public class CarParkingPlaceRepository(DataContext context) : ICarParkingPlaceRepository
    {
        private readonly DataContext _context = context;

        public async Task AddAsync(CarParkingPlace item)
        {
            _context.CarParkingPlaces.Add(item);
            await _context.SaveChangesAsync();
        }

        public Task<List<CarParkingPlace>> GetAllAsync()
             => _context.CarParkingPlaces.ToListAsync();

        public Task<CarParkingPlace> GetByIdAsync(Guid itemId)
            => _context.CarParkingPlaces.FirstOrDefaultAsync(x => x.CarId == itemId);
    }
}