using AutoMapper;
using Pixicity.Data.Models.Parametros;
using Pixicity.Domain.ViewModels.Parametros;

namespace Pixicity.Data.Mappings.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pais, PaisViewModel>()
                //.ForMember(dest => dest.Nombre, source => source.MapFrom(source => source.Nombre))
                .ReverseMap();
        }
    }
}
