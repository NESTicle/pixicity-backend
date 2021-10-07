﻿using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.ViewModels.Seguridad;
using System;
using System.Collections.Generic;

namespace Pixicity.Service.Interfaces
{
    public interface ISeguridadService
    {
        /// <summary>
        /// Obtiene una lista de usuarios
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<Usuario> GetUsuarios(QueryParamsHelper queryParameters, out long totalCount);

        /// <summary>
        /// Obtiene la cantidad de Usuarios registrados en el sistema
        /// </summary>
        /// <returns></returns>
        long CountUsuarios();

        /// <summary>
        /// Obtiene un usuario filtrado por el userName
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns></returns>
        Usuario GetUsuarioByUserName(string userName);

        /// <summary>
        /// Obtiene un usuario con la información relacionada, filtrado por el userName
        /// </summary>
        /// <param name="userName">Nombre del usuario</param>
        /// <returns></returns>
        Usuario GetUsuarioInfoByUserName(string userName);

        /// <summary>
        /// Obtiene un usuario filtrado por el Id
        /// </summary>
        /// <param name="id">Id del Usuario</param>
        /// <returns></returns>
        Usuario GetUsuarioById(long id);

        /// <summary>
        /// Crear un nuevo usuario en el sistema
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        long RegistrarUsuario(UsuarioViewModel usuario);

        /// <summary>
        /// Actualizar usuario en el sistema
        /// </summary>
        /// <param name="usuario">Entidad Usuario</param>
        /// <returns></returns>
        long UpdateUsuario(Usuario usuario);

        /// <summary>
        /// Iniciar sesión con la cuenta del Usuario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Usuario LoginUsuario(UsuarioViewModel model);

        /// <summary>
        /// Generar JWT token para el Usuario logeado
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string GenerarJWT(Usuario model);

        /// <summary>
        /// Sumar puntos para el Usuario
        /// </summary>
        /// <param name="usuarioId">Id del Usuario</param>
        /// <param name="puntos">Puntos que quieres sumarle al Usuario</param>
        /// <returns></returns>
        int SumarPuntosUsuario(long usuarioId, int puntos);

        /// <summary>
        /// Obtener el usuario logeado
        /// </summary>
        /// <returns></returns>
        Usuario GetLoggedUserByJwt();

        /// <summary>
        /// Cambiar la contraseña actual del usuario
        /// </summary>
        /// <param name="model">viewModel ChangePasswordUsuarioViewModel</param>
        /// <returns></returns>
        bool ChangeUserPassword(ChangePasswordUsuarioViewModel model);

        /// <summary>
        /// Obtiene la lista de Sessiones creadas para los Usuarios
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<Session> GetSesiones(QueryParamsHelper queryParameters, out long totalCount);

        /// <summary>
        /// Obtener una sesión filtrada por el Id del registro
        /// </summary>
        /// <param name="id">Id de la Session</param>
        /// <returns></returns>
        Session GetSessionById(long id);

        /// <summary>
        /// Obtiene una sesión filtrada por el Token
        /// </summary>
        /// <param name="token">JWT Token string</param>
        /// <returns></returns>
        Session GetSessionByToken(string token);

        /// <summary>
        /// Actualizar el Campo de Activo de la sesión para determinar si se encuentra o no activo el Usuario
        /// </summary>
        /// <param name="model">Entidad Session</param>
        /// <returns></returns>
        DateTime UpdateSessionActivoDate(Session model);

        /// <summary>
        /// Guarda la sesión jwt en la base de datos
        /// </summary>
        /// <param name="model">Entidad Session</param>
        /// <returns></returns>
        long SaveUserSession(Session model);

        /// <summary>
        /// Eliminar la sesión del usuario
        /// </summary>
        /// <param name="id">Id de la sesión</param>
        /// <returns></returns>
        bool DeleteSession(long id);
    }
}
