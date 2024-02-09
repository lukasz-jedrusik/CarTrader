using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTrader.Services.Workflow.Domain.Models;

namespace CarTrader.Services.Workflow.Application.Interfaces.Repositories
{
    public interface ICarProcessRepository
    {
        Task AddAsync(CarProcess item);
        Task<CarProcess> GetByIdAsync(Guid itemId);
    }
}