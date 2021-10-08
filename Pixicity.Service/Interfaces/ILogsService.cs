using Pixicity.Data.Models.Logs;
using Pixicity.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Service.Interfaces
{
    public interface ILogsService
    {
        /// <summary>
        /// Crear un log del sistema
        /// </summary>
        /// <param name="model">Entidad SystemLogs</param>
        /// <returns></returns>
        long CreateSystemLogs(SystemLogs model);

        /// <summary>
        /// Crea una notificación para el Usuario
        /// </summary>
        /// <param name="monitor">Entidad Monitor</param>
        /// <returns></returns>
        long SaveMonitor(Monitor monitor);

        /// <summary>
        /// Obtener la lista de notificaciones del usuario actual
        /// </summary>
        /// <param name="queryParameters">Parámetros para filtrar la tabla</param>
        /// <param name="totalCount">Total de notificaciones</param>
        /// <returns></returns>
        List<Monitor> GetNotificacionesByCurrentUser(QueryParamsHelper queryParameters, out long totalCount);
    }
}
