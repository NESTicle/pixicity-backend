using Microsoft.EntityFrameworkCore;
using Pixicity.Data;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.Helpers;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pixicity.Service.Implementations
{
    public class WebService : IWebService
    {
        private readonly PixicityDbContext _dbContext;

        public WebService(PixicityDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
