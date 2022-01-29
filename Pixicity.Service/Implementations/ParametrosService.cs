using Microsoft.EntityFrameworkCore;
using Pixicity.Data;
using Pixicity.Data.Models.Parametros;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.Transversal;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pixicity.Service.Implementations
{
    public class ParametrosService : IParametrosService
    {
        private readonly PixicityDbContext _dbContext;
        private IAppPrincipal _currentUser { get; }

        public ParametrosService(PixicityDbContext dbContext, IAppPrincipal currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public List<Pais> GetPaises(QueryParamsHelper queryParameters, out long totalCount)
        {
            try
            {
                var posts = _dbContext.Pais
                    .AsNoTracking()
                    .AsQueryable();

                totalCount = posts.Count();

                return posts
                    .OrderBy(x => x.Nombre)
                    .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                    .Take(queryParameters.PageCount)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Pais GetPaisById(long id)
        {
            try
            {
                return _dbContext.Pais.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Pais> GetPaisesDropdown()
        {
            try
            {
                return _dbContext.Pais.AsNoTracking()
                    .Where(x => x.Eliminado == false)
                    .OrderBy(x => x.Nombre)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long SavePais(Pais model)
        {
            _dbContext.Pais.Add(model);
            _dbContext.SaveChanges();

            return model.Id;
        }

        public long UpdatePais(Pais model)
        {
            try
            {
                Pais pais = GetPaisById(model.Id);
                pais.ISO2 = model.ISO2;
                pais.ISO3 = model.ISO3;
                pais.Nombre = model.Nombre;
                pais.FechaActualiza = DateTime.Now;
                pais.UsuarioActualiza = _currentUser.UserName;

                _dbContext.Update(pais);
                _dbContext.SaveChanges();

                return model.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Estado> GetEstadosByPaisId(long IdPais)
        {
            try
            {
                return _dbContext.Estado.AsNoTracking()
                    .Where(x => x.IdPais == IdPais && x.Eliminado == false)
                    .OrderBy(x => x.Nombre)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long SaveEstado(Estado model)
        {
            _dbContext.Estado.Add(model);
            _dbContext.SaveChanges();

            return model.Id;
        }

        public Rango GetRangoByNombre(string rango)
        {
            try
            {
                return _dbContext.Rango.FirstOrDefault(x => x.Nombre.ToLower().Equals(rango.Trim().ToLower()) && x.Eliminado == false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long CreateRangoIfNotExists(string nombre)
        {
            try
            {
                Rango rango = GetRangoByNombre(nombre);

                if (rango != null)
                    return rango.Id;

                rango = new Rango()
                {
                    Id = 0,
                    Nombre = nombre.Trim(),
                    Icono = ""
                };

                _dbContext.Rango.Add(rango);
                _dbContext.SaveChanges();

                return rango.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Categoria> GetCategoriasDropdown()
        {
            try
            {
                return _dbContext.Categoria
                    .AsNoTracking()
                    .Where(x => x.Eliminado == false)
                    .OrderBy(x => x.Nombre)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long SaveCategoria(Categoria model)
        {
            try
            {
                Categoria categoria = new Categoria()
                {
                    Nombre = model.Nombre.Trim(),
                    SEO = model.SEO.Trim().ToLower(),
                    Icono = model.Icono
                };

                _dbContext.Categoria.Add(categoria);
                _dbContext.SaveChanges();

                return categoria.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int PopulateCategorias()
        {
            try
            {
                /*
                    (33, 33, '', '', 'film.png');
                 */

                var categoriesList = _dbContext.Categoria.Where(x => x.Eliminado == false);

                if (categoriesList.Count() > 0)
                    return categoriesList.Count();

                List<Categoria> categorias = new List<Categoria>
                {
                    new Categoria()
                    {
                        Nombre = "Animaciones",
                        SEO = "animaciones",
                        Icono = "animaciones.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Apuntes y Monografías",
                        SEO = "apuntes-monografias",
                        Icono = "apuntes-monografias.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Arte",
                        SEO = "arte",
                        Icono = "arte.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Autos y Motos",
                        SEO = "autos-motos",
                        Icono = "autos-motos.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Celulares",
                        SEO = "celulares",
                        Icono = "celulares.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Ciencia y Educación",
                        SEO = "ciencia-educacion",
                        Icono = "ciencia-educacion.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Comics",
                        SEO = "comics",
                        Icono = "comics.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Deportes",
                        SEO = "deportes",
                        Icono = "deportes.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Downloads",
                        SEO = "downloads",
                        Icono = "downloads.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "E-books y Tutoriales",
                        SEO = "ebooks-tutoriales",
                        Icono = "ebooks-tutoriales.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Ecología",
                        SEO = "ecologia",
                        Icono = "ecologia.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Economía y Negocios",
                        SEO = "economia-negocios",
                        Icono = "economia-negocios.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Femme",
                        SEO = "femme",
                        Icono = "femme.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Hazlo tu mismo",
                        SEO = "hazlotumismo",
                        Icono = "hazlotumismo.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Humor",
                        SEO = "humor",
                        Icono = "humor.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Imágenes",
                        SEO = "imagenes",
                        Icono = "imagenes.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Info",
                        SEO = "info",
                        Icono = "info.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Juegos",
                        SEO = "juegos",
                        Icono = "juegos.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Links",
                        SEO = "links",
                        Icono = "links.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Linux",
                        SEO = "linux",
                        Icono = "tux.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Mac",
                        SEO = "mac",
                        Icono = "mac.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Manga y Anime",
                        SEO = "manga-anime",
                        Icono = "manga-anime.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Mascotas",
                        SEO = "mascotas",
                        Icono = "mascotas.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Música",
                        SEO = "musica",
                        Icono = "musica.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Noticias",
                        SEO = "noticias",
                        Icono = "noticias.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Off Topic",
                        SEO = "offtopic",
                        Icono = "offtopic.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Recetas y Cocina",
                        SEO = "recetas-cocina",
                        Icono = "recetas-cocina.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Salud y Bienestar",
                        SEO = "salud-bienestar",
                        Icono = "salud-bienestar.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Solidaridad",
                        SEO = "solidaridad",
                        Icono = "solidaridad.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Pixicity!",
                        SEO = "pixicity",
                        Icono = "pixicity.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Turismo",
                        SEO = "turismo",
                        Icono = "turismo.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "TV, Peliculas y Series",
                        SEO = "tv-peliculas-series",
                        Icono = "tv-peliculas-series.png",
                        UsuarioRegistra = "SYSTEM"
                    },

                    new Categoria()
                    {
                        Nombre = "Videos On-line!",
                        SEO = "videos-online",
                        Icono = "videos-online.png",
                        UsuarioRegistra = "SYSTEM"
                    },
                };

                _dbContext.Categoria.AddRange(categorias);
                return _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
