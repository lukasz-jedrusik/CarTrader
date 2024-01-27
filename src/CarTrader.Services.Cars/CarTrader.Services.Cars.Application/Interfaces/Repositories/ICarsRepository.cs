using CarTrader.Services.Cars.Domain.Models;

namespace CarTrader.Services.Cars.Application.Interfaces.Repositories
{
    public interface ICarsRepository
    {
        Task AddAsync(Car item);
        Task<int> GetNumberAsync();
        Task<Car> GetByIdAsync(Guid itemId);
        Task UpdateAsync(Car item);
    }
}
