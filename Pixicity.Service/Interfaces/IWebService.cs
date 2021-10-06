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
    }
}
