using AutoMapper;
using CarTrader.Services.Cars.Application.DataTransferObjects;
using CarTrader.Services.Cars.Domain.Models;

namespace CarTrader.Services.Cars.Application.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Car, CarDto>().ReverseMap();
        }
    }
}