using Microsoft.EntityFrameworkCore;
using Pixicity.Data;
using Pixicity.Data.Models.Posts;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.ViewModels.Web;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pixicity.Service.Implementations
{
    public class WebService : IWebService
    {
        private readonly PixicityDbContext _dbContext;
        private readonly IPostService _postService;

        public WebService(PixicityDbContext dbContext, IPostService postService)
        {
            _dbContext = dbContext;
            _postService = postService;
        }

        public string SaveAfiliado(Afiliado model)
        {
            try
            {
                model.Titulo = model.Titulo.Trim();
                model.URL = model.URL.Trim().ToLower();
                model.Descripcion = model.Descripcion.Trim();
                model.Banner = model.Banner.Trim().ToLower();
                model.Codigo = StringHelper.RandomString(10);
                model.Activo = false;

                _dbContext.Afiliado.Add(model);
                _dbContext.SaveChanges();

                return model.Codigo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Afiliado> GetAfiliados(QueryParamsHelper queryParameters, out long totalCount)
        {
            try
            {
                var posts = _dbContext.Afiliado
                   .AsNoTracking()
                   .AsQueryable();

                totalCount = posts.Count();

                return posts
                    .OrderByDescending(x => x.FechaRegistro)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Configuracion GetConfiguracion()
        {
            try
            {
                return _dbContext.Configuracion.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Configuracion CreateConfiguracion(Configuracion model)
        {
            try
            {
                Configuracion config = new Configuracion()
                {
                    DateCreated = DateTime.Now,
                    SiteName = model.SiteName,
                    Slogan = model.Slogan,
                    URL = model.URL
                };

                _dbContext.Configuracion.Add(config);
                _dbContext.SaveChanges();

                return config;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UpdateConfiguracion(Configuracion model)
        {
            try
            {
                Configuracion configuracion = GetConfiguracion();

                if (configuracion == null)
                    configuracion = CreateConfiguracion(model);

                configuracion.Slogan = model.Slogan;
                configuracion.SiteName = model.SiteName;
                configuracion.OnlineUsersTime = model.OnlineUsersTime;
                configuracion.MaintenanceMode = model.MaintenanceMode;
                configuracion.MaintenanceMessage = model.MaintenanceMessage;
                configuracion.DisableUserRegistration = model.DisableUserRegistration;
                configuracion.DisableUserRegistrationMessage = model.DisableUserRegistrationMessage;

                _dbContext.Update(configuracion);
                return _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int UpdateAds(Configuracion model)
        {
            try
            {
                Configuracion configuracion = GetConfiguracion();

                if (configuracion == null)
                    configuracion = CreateConfiguracion(model);

                configuracion.HeaderScript = model.HeaderScript;
                configuracion.FooterScript = model.FooterScript;
                configuracion.Banner160x600 = model.Banner160x600;
                configuracion.Banner300x250 = model.Banner300x250;
                configuracion.Banner468x60 = model.Banner468x60;
                configuracion.Banner728x90 = model.Banner728x90;

                _dbContext.Update(configuracion);
                return _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<TopUsuarioViewModel> GetTopUsuarios()
        {
            try
            {
                var users = _dbContext.Usuario
                    .AsNoTracking()
                    .Where(x => x.Eliminado == false && x.Puntos > 0)
                    .OrderByDescending(x => x.Puntos)
                    .Take(10);

                var data = users.Select(x => new TopUsuarioViewModel()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Puntos = x.Puntos,
                });

                return data.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Post> GetTopPosts(string date)
        {
            try
            {
                var posts = _dbContext.Post
                    .Include(x => x.Categoria)
                    .AsNoTracking()
                    .Where(x => x.Eliminado == false)
                    .AsQueryable();

                posts = _postService.FilterTopPosts(date, posts)
                    .OrderByDescending(s => s.Puntos)
                    .Take(10);

                return posts.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetAdsByType(string type)
        {
            try
            {
                Configuracion configuracion = _dbContext.Configuracion.FirstOrDefault();

                if (configuracion == null)
                    return string.Empty;

                return type switch
                {
                    "160x600" => configuracion.Banner160x600,
                    "300x250" => configuracion.Banner300x250,
                    "468x60" => configuracion.Banner468x60,
                    "728x90" => configuracion.Banner728x90,
                    _ => configuracion.Banner160x600,
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Afiliado> GetAfiliados()
        {
            try
            {
                var afiliados = _dbContext.Afiliado
                    .AsNoTracking()
                    .Where(x => x.Eliminado == false && x.Activo == true)
                    .ToList();

                return afiliados;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int ChangeAfiliadoActive(Afiliado model)
        {
            try
            {
                Afiliado afiliado = _dbContext.Afiliado.FirstOrDefault(x => x.Id == model.Id && x.Eliminado == false);

                if (afiliado == null)
                    throw new Exception($"Un error se ha encontrado al tratar de cambiar el estado al Afiliado con Id {model.Id}");

                afiliado.Activo = !afiliado.Activo;
                _dbContext.Update(afiliado);

                return _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string HitAfiliado(string codigo)
        {
            try
            {
                Afiliado afiliado = _dbContext.Afiliado.FirstOrDefault(x => x.Codigo == codigo);

                if (afiliado == null)
                    return string.Empty;

                afiliado.HitsOut++;

                _dbContext.Update(afiliado);
                _dbContext.SaveChanges();

                return afiliado.URL;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
