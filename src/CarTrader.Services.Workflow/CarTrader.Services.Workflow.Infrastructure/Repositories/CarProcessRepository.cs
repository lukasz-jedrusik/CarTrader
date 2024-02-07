using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarTrader.Services.Workflow.Application.Interfaces.Repositories;
using CarTrader.Services.Workflow.Domain.Models;
using CarTrader.Services.Workflow.Infrastructure.Extensions.EfCore;

namespace CarTrader.Services.Workflow.Infrastructure.Repositories
{
    public class CarProcessRepository(DataContext context) : ICarProcessRepository
    {
        private readonly DataContext _context = context;

        public async Task AddAsync(CarProcess item)
        {
            _context.CarProcesses.Add(item); 
            await _context.SaveChangesAsync();
        }
    }
}