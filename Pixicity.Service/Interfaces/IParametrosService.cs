using Pixicity.Data.Models.Parametros;
using Pixicity.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Service.Interfaces
{
    public interface IParametrosService
    {
        /// <summary>
        /// Obtener la lista de paises para visualizarlos en el dashboard
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <returns></returns>
        List<Pais> GetPaises(QueryParamsHelper queryParameters, out long totalCount);

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

        /// <summary>
        /// Obtiene un Rango por el Nombre
        /// </summary>
        /// <param name="rango">Nombre del Rango</param>
        /// <returns></returns>
        Rango GetRangoByNombre(string rango);

        /// <summary>
        /// Devuelve el Id del Rango que se va a crear (si no existe) o el Id del Rango ya creado
        /// </summary>
        /// <param name="rango">Nombre del Rango</param>
        /// <returns></returns>
        long CreateRangoIfNotExists(string rango);

        /// <summary>
        /// Obtiene la lista completa de Categorias
        /// </summary>
        /// <returns></returns>
        List<Categoria> GetCategoriasDropdown();

        /// <summary>
        /// Guarda la categoria
        /// </summary>
        /// <param name="model">Entidad Categoria</param>
        /// <returns></returns>
        long SaveCategoria(Categoria model);
    }
}
