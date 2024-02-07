using CarTrader.Services.Workflow.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarTrader.Services.Workflow.Infrastructure.Extensions.EfCore
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<CarProcess> CarProcesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarProcess>()
                .HasKey(e => e.CarId);

            modelBuilder.Entity<CarProcess>()
                .Property(x => x.CarId)
                .HasConversion(new GuidToStringConverter());

            modelBuilder.Entity<CarProcess>()
                .Property(x => x.CamundaProcessId)
                .HasConversion<string>()
                .HasMaxLength(50);
        }
    }
}