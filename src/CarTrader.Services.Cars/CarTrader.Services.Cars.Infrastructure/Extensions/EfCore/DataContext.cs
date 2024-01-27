using CarTrader.Services.Cars.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarTrader.Services.Cars.Infrastructure.Extensions.EfCore
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(x => x.Id)
                .HasConversion(new GuidToStringConverter());

            modelBuilder.Entity<Car>()
                .Property(x => x.BodyType)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<Car>()
                .Property(x => x.WheelDriveType)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<Car>()
                .Property(x => x.FuelType)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<Car>()
                .Property(x => x.TransmissionType)
                .HasConversion<string>()
                .HasMaxLength(50);
        }
    }
}