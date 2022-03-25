using Microsoft.EntityFrameworkCore;
using Pixicity.Data;
using Pixicity.Data.Models.Logs;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.Transversal;
using Pixicity.Domain.ViewModels.Logs;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixicity.Service.Implementations
{
    public class LogsService : ILogsService
    {
        private readonly PixicityDbContext _dbContext;
        private IAppPrincipal _currentUser { get; }

        public LogsService(PixicityDbContext dbContext, IAppPrincipal currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public long CreateSystemLogs(SystemLogs model)
        {
            try
            {
                //if (!string.IsNullOrEmpty(model.Code))
                //    model.Code = model.Code.Trim().ToUpper();

                //_dbContext.SystemLogs.Add(model);
                //_dbContext.SaveChanges();

                //return model.Id;

                return 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long SaveMonitor(Monitor monitor)
        {
            try
            {
                _dbContext.Monitor.Add(monitor);
                _dbContext.SaveChanges();

                return monitor.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Monitor> GetLastNotificacionesByCurrentUser()
        {
            try
            {
                return _dbContext.Monitor
                    .AsNoTracking()
                    .Include(x => x.Usuario)
                    .Include(x => x.UsuarioQueHaceAccion)
                    .Include(x => x.Post.Categoria)
                    .Where(x => x.Eliminado == false && x.UsuarioId == _currentUser.Id)
                    .OrderByDescending(x => x.FechaRegistro)
                    .Take(10)
                    .ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool SetNotificacionesAsReaded()
        {
            try
            {
                List<Monitor> notificaciones = _dbContext.Monitor
                    .Where(x => x.Eliminado == false && x.UsuarioId == _currentUser.Id && x.Leido == false)
                    .ToList();

                if (notificaciones == null || notificaciones.Count < 1)
                    return false;

                foreach (var notificacion in notificaciones)
                {
                    notificacion.Leido = true;
                }

                _dbContext.UpdateRange(notificaciones);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Monitor> GetNotificacionesByCurrentUser(QueryParamsHelper queryParameters, out long totalCount)
        {
            try
            {
                var query = _dbContext.Monitor
                    .AsNoTracking()
                    .Include(x => x.Usuario)
                    .Include(x => x.UsuarioQueHaceAccion)
                    .Include(x => x.Post.Categoria)
                    .Where(x => x.Eliminado == false && x.UsuarioId == _currentUser.Id);

                totalCount = query.Count();

                return query
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

        public StatsViewModel GetCurrentUserStats()
        {
            try
            {
                var notifications = _dbContext.Monitor
                    .Count(x => x.Eliminado == false && x.Leido == false && x.UsuarioId == _currentUser.Id);

                return new StatsViewModel()
                {
                    Notifications = notifications,
                    Messages = 101
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
