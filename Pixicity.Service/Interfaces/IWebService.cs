using Pixicity.Data.Models.Posts;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.ViewModels.Web;
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

        /// <summary>
        /// Actualiza la información de la publicidad
        /// </summary>
        /// <param name="model">Entidad Configuracion</param>
        /// <returns></returns>
        int UpdateAds(Configuracion model);

        List<TopUsuarioViewModel> GetTopUsuarios();

        List<Post> GetTopPosts(string date);

        /// <summary>
        /// Obtiene el banner de ads de la tabla de Configuración
        /// </summary>
        /// <param name="type">Tipo de banner</param>
        /// <returns></returns>
        string GetAdsByType(string type);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Afiliado> GetAfiliados();

        /// <summary>
        /// Cambiar el estado de 'Activo' de la entidad Afiliado
        /// </summary>
        /// <param name="model">Entidad Afiliado</param>
        /// <returns></returns>
        int ChangeAfiliadoActive(Afiliado model);

        /// <summary>
        /// Sumar visita al afiliado
        /// </summary>
        /// <param name="codigo">Código del afiliado</param>
        /// <returns></returns>
        string HitAfiliado(string codigo);

        /// <summary>
        /// Obtener la lista de los últimos cambios en el histórico de moderación
        /// </summary>
        /// <returns></returns>
        List<HistorialViewModel> GetHistorialModeracion();

        /// <summary>
        /// Actualizar la afiliación
        /// </summary>
        /// <param name="model">Entidad Afiliado</param>
        /// <returns></returns>
        long UpdateAfiliacion(Afiliado model);

        /// <summary>
        /// Sumar una visita a Pixicity
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        long SetHitIn(string codigo);

        /// <summary>
        /// Eliminar un afiliado
        /// </summary>
        /// <param name="id">Id del afiliado</param>
        /// <returns></returns>
        long DeleteAfiliado(long id);

        /// <summary>
        /// Guardar formulario de contacto de la comunidad
        /// </summary>
        /// <param name="contacto">Entidad Contacto</param>
        /// <returns></returns>
        long SaveContacto(Contacto contacto);
    }
}
