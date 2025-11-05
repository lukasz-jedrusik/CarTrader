using CarTrader.Services.Diagnostic.Domain.Models;

namespace CarTrader.Services.Diagnostic.Application.Interfaces.Repositories
{
    public interface ICarDiagnosticRepository
    {
        Task AddAsync(CarDiagnostic item);
        Task<CarDiagnostic> GetByIdAsync(Guid itemId);
    }
}