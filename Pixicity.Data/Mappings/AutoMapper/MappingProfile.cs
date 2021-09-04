using AutoMapper;
using Pixicity.Data.Models.Parametros;
using Pixicity.Data.Models.Posts;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Domain.ViewModels.Parametros;
using Pixicity.Domain.ViewModels.Posts;

namespace Pixicity.Data.Mappings.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pais, PaisViewModel>()
                //.ForMember(dest => dest.Nombre, source => source.MapFrom(source => source.Nombre))
                .ReverseMap();

            CreateMap<Post, PostViewModel>();
            CreateMap<Categoria, CategoriaViewModel>();

            CreateMap<Pais, DropdownViewModel>();
            CreateMap<Estado, DropdownViewModel>();
            CreateMap<Categoria, DropdownViewModel>();
            CreateMap<FavoritoPost, FavoritosViewModel>()
                .ForMember(des => des.FechaRegistro, source => source.MapFrom(s => s.FechaRegistro))
                .ForMember(des => des.Post, source => source.MapFrom(s => s.Post));
        }
    }
}
