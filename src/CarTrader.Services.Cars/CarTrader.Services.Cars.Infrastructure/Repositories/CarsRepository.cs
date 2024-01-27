using CarTrader.Services.Cars.Application.Interfaces.Repositories;
using CarTrader.Services.Cars.Domain.Models;
using CarTrader.Services.Cars.Infrastructure.Extensions.EfCore;
using Microsoft.EntityFrameworkCore;

namespace CarTrader.Services.Cars.Infrastructure.Repositories
{
    public class CarsRepository(DataContext context) : ICarsRepository
    {
        private readonly DataContext _context = context;

        public async Task AddAsync(Car item)
        {
            _context.Cars.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetNumberAsync()
        {
            int? dbval = await _context.Cars.Where(x => x.Year == DateTime.UtcNow.Year)
                .MaxAsync(y => (int?)y.Number);

            if (dbval == null)
            {
                return 1;
            }

            return ((int)dbval) + 1;
        }

        public Task<Car> GetByIdAsync(Guid itemId) =>
            _context.Cars.FirstOrDefaultAsync(x => x.Id == itemId);

        public async Task UpdateAsync(Car item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
