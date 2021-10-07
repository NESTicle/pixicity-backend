﻿using AutoMapper;
using Pixicity.Data.Models.Parametros;
using Pixicity.Data.Models.Posts;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Domain.ViewModels.Parametros;
using Pixicity.Domain.ViewModels.Posts;
using Pixicity.Domain.ViewModels.Seguridad;

namespace Pixicity.Data.Mappings.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pais, PaisViewModel>()
                //.ForMember(dest => dest.Nombre, source => source.MapFrom(source => source.Nombre))
                .ReverseMap();

            CreateMap<Post, PostViewModel>()
                .ForMember(des => des.URL, source => source.MapFrom(s => s.Titulo.ToLower().Replace(" ", "-")))
                .ReverseMap();

            CreateMap<Categoria, CategoriaViewModel>()
                .ReverseMap();

            CreateMap<Pais, DropdownViewModel>();
            CreateMap<Estado, DropdownViewModel>();
            CreateMap<Categoria, DropdownViewModel>();
            CreateMap<FavoritoPost, FavoritosViewModel>()
                .ForMember(des => des.FechaRegistro, source => source.MapFrom(s => s.FechaRegistro))
                .ForMember(des => des.Post, source => source.MapFrom(s => s.Post));

            CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(des => des.PaisId, source => source.MapFrom(s => s.Estado != null ? s.Estado.IdPais : (long?)null))
                .ForMember(des => des.Password, source => source.Ignore())
                .ReverseMap();

            CreateMap<Comentario, ComentarioViewModel>()
                .ForMember(des => des.Usuario, source => source.MapFrom(s => s.Usuario.UserName))
                .ForMember(des => des.Post, source => source.MapFrom(s => s.Post))
                .ReverseMap();
        }
    }
}
