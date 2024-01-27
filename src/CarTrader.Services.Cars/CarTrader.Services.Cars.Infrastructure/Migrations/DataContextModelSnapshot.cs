﻿// <auto-generated />
using System;
using CarTrader.Services.Cars.Infrastructure.Extensions.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarTrader.Services.Cars.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarTrader.Services.Cars.Domain.Models.Car", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("BodyType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CamundaProcessId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreateUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EngineSize")
                        .HasColumnType("int");

                    b.Property<string>("FuelType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Manfacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Mileage")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("Power")
                        .HasColumnType("int");

                    b.Property<string>("TransmissionType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WheelDriveType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<int>("YearOfProduction")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
