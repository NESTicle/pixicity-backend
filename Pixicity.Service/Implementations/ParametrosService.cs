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
    }
}
