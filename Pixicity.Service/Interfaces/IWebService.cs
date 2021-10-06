using Pixicity.Data.Models.Web;
using Pixicity.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Service.Interfaces
{
    public interface IWebService
    {
        /// <summary>
        /// Se guarda un afiliado
        /// </summary>
        /// <param name="model">Entidad Afiliado</param>
        /// <returns></returns>
        string SaveAfiliado(Afiliado model);

        /// <summary>
        /// Obtiene una lista de Afiliados para visualizar en el Dashboard
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        List<Afiliado> GetAfiliados(QueryParamsHelper queryParameters, out long totalCount);

        /// <summary>
        /// Obtiene la Configuración actual del sitio
        /// </summary>
        /// <returns></returns>
        Configuracion GetConfiguracion();

        /// <summary>
        /// Crear una configuración para el sitio
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Configuracion CreateConfiguracion(Configuracion model);

        /// <summary>
        /// Actualiza la información de la configuración del sitio
        /// </summary>
        /// <param name="model">Entidad Configuracion</param>
        /// <returns></returns>
        int UpdateConfiguracion(Configuracion model);
    }
}
