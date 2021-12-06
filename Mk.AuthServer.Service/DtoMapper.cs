using AutoMapper;
using Mk.AuthServer.Core.Dtos;
using Mk.AuthServer.Core.models;

namespace Mk.AuthServer.Service
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<UserApp, UserAppDto>().ReverseMap();
        }
        
    }
}