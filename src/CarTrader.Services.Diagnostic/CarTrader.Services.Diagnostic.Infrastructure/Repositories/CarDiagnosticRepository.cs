using CarTrader.Services.Diagnostic.Application.Interfaces.Repositories;
using CarTrader.Services.Diagnostic.Domain.Exceptions;
using CarTrader.Services.Diagnostic.Domain.Models;
using CarTrader.Services.Diagnostic.Infrastructure.Extensions.EfCore;
using Microsoft.EntityFrameworkCore;

namespace CarTrader.Services.Diagnostic.Infrastructure.Repositories
{
    public class CarDiagnosticRepository(DataContext context)
        : ICarDiagnosticRepository
    {
        private readonly DataContext _context = context;

        public async Task AddAsync(CarDiagnostic item)
        {
            try
            {
                _context.CarDiagnostics.Add(item);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new RegisterCarInDiagnosticException(item.CarId, item.BussinesKey);
            }
        }

        public Task<CarDiagnostic> GetByIdAsync(Guid itemId)
            => _context.CarDiagnostics.FirstOrDefaultAsync(x => x.CarId == itemId);
    }
}