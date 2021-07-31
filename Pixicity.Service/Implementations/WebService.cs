using Pixicity.Data;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.Helpers;
using Pixicity.Service.Interfaces;
using System;

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
    }
}
