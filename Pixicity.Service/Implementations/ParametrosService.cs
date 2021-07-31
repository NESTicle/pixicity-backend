using Microsoft.EntityFrameworkCore;
using Pixicity.Data.Models.Base;
using Pixicity.Data.Models.Parametros;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixicity.Service.Implementations
{
    public class ParametrosService : IParametrosService
    {
        private readonly PixicityDbContext _dbContext;

        public ParametrosService(PixicityDbContext dbContext)
        {
            _dbContext = dbContext;
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
