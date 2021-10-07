using Pixicity.Data.Models.Logs;
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
    }
}
