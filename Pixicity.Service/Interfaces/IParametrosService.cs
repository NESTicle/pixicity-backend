using Pixicity.Data.Models.Parametros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Service.Interfaces
{
    public interface IParametrosService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Pais> GetPaisesDropdown();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pais"></param>
        /// <returns></returns>
        long SavePais(Pais pais);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPais"></param>
        /// <returns></returns>
        List<Estado> GetEstadosByPaisId(long IdPais);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        long SaveEstado(Estado estado);
    }
}
