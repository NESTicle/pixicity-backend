using AutoMapper;
using Pixicity.Data.Models.Logs;
using Pixicity.Data.Models.Parametros;
using Pixicity.Data.Models.Posts;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.ViewModels.Base;
using Pixicity.Domain.ViewModels.Logs;
using Pixicity.Domain.ViewModels.Parametros;
using Pixicity.Domain.ViewModels.Posts;
using Pixicity.Domain.ViewModels.Seguridad;
using Pixicity.Domain.ViewModels.Web;
using System;
using System.Linq;

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
                .ForMember(des => des.Comentarios, source => source.MapFrom(s => s.Comentarios.Count))
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
                .ForMember(des => des.Password, source => source.Ignore());

            CreateMap<UsuarioViewModel, Usuario>()
                .ForMember(des => des.GeneroString, source => source.Ignore());

            CreateMap<Comentario, ComentarioViewModel>()
                .ForMember(des => des.Usuario, source => source.MapFrom(s => s.Usuario.UserName))
                .ForMember(des => des.Post, source => source.MapFrom(s => s.Post))
                .ReverseMap();

            CreateMap<Session, SessionViewModel>()
                .ForMember(des => des.Usuario, source => source.MapFrom(s => s.Usuario.UserName))
                .ReverseMap();

            CreateMap<Monitor, MonitorViewModel>()
                .ForMember(des => des.UsuarioQueHaceAccion, source => source.MapFrom(s => s.UsuarioQueHaceAccion))
                .ReverseMap();

            CreateMap<Denuncia, DenunciaViewModel>()
                .ForMember(des => des.UsuarioDenuncia, source => source.MapFrom(s => s.Usuario.UserName));

            CreateMap<Post, TopPostsViewModel>()
                .ForMember(des => des.URL, source => source.MapFrom(s => s.Titulo.ToLower().Replace(" ", "-")));

            CreateMap<Usuario, PerfilUsuarioViewModel>()
                .ForMember(des => des.Pais, source => source.MapFrom(s => s.Estado.Pais))
                .ForMember(des => des.Genero, source => source.MapFrom(s => s.GeneroString))
                .ForMember(des => des.CompleteName, source => source.MapFrom(s => s.UsuarioPerfil.FirstOrDefault().CompleteName));

            CreateMap<Estado, EstadoViewModel>();

            CreateMap<Usuario, UsuarioAdminViewModel>()
                .ForMember(des => des.Genero, source => source.MapFrom(s => s.GeneroString));

            CreateMap<Rango, RangoViewModel>();
            CreateMap<Rango, RangoUsuarioViewModel>();
            CreateMap<Rango, DropdownViewModel>();
            CreateMap<UsuarioPerfil, UsuarioPerfilViewModel>();
        }
    }
}
