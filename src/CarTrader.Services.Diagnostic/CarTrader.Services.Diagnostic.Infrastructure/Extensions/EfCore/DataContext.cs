using CarTrader.Services.Diagnostic.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarTrader.Services.Diagnostic.Infrastructure.Extensions.EfCore
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<CarDiagnostic> CarDiagnostics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarDiagnostic>()
                .HasKey(e => e.CarId);

            modelBuilder.Entity<CarDiagnostic>()
                .Property(x => x.CarId)
                .HasConversion(new GuidToStringConverter());

            modelBuilder.Entity<CarDiagnostic>()
                .Property(x => x.OverallCarCondition)
                .HasConversion<string>()
                .HasMaxLength(50);
        }
    }
}