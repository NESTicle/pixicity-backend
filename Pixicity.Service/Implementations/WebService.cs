﻿using Microsoft.EntityFrameworkCore;
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

                configuracion.SiteName = model.SiteName;
                configuracion.Slogan = model.Slogan;
                configuracion.MaintenanceMode = model.MaintenanceMode;
                configuracion.MaintenanceMessage = model.MaintenanceMessage;
                configuracion.OnlineUsersTime = model.OnlineUsersTime;

                _dbContext.Update(configuracion);
                return _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
