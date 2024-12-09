using AutoMapper;
using Winecellar.Application.Dtos.Wines;
using Winecellar.Domain.Models;

namespace Winecellar.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Wine, WineDto>();
        }
    }
}