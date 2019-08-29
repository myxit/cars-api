using System;
using AntilopaApi.Data;
using AntilopaApi.Models;
using AutoMapper;

namespace Antilopa.MappingProfiles
{
    public class CarMappingProfile : Profile
    {
        public class OwnerIdOptConverter : ITypeConverter<Car, CarViewModel>
{
        public CarViewModel Convert(Car source, CarViewModel destination, ResolutionContext context)
        {
            destination.OwnerId = (int)context.Items["OwnerId"];
            return destination;
        }
    }
        public CarMappingProfile()
        {
            CreateMap<Car, CarViewModel>();
            CreateMap<CarInputModel, Car>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}