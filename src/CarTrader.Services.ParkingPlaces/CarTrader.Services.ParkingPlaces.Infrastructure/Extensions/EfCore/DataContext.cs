using CarTrader.Services.ParkingPlaces.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarTrader.Services.ParkingPlaces.Infrastructure.Extensions.EfCore
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<CarParkingPlace> CarParkingPlaces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarParkingPlace>()
                .HasKey(e => e.CarId);

            modelBuilder.Entity<CarParkingPlace>()
                .Property(x => x.CarId)
                .HasConversion(new GuidToStringConverter());
        }
    }
}