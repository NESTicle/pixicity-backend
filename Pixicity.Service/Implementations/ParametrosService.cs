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
                    .Where(x => x.Activo)
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
                    .Where(x => x.IdPais == IdPais && x.Activo)
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
    }
}
