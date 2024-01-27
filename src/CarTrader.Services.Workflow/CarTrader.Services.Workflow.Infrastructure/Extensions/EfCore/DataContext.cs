using Microsoft.EntityFrameworkCore;

namespace CarTrader.Services.Workflow.Infrastructure.Extensions.EfCore
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}