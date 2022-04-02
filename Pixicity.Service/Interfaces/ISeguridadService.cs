using Pixicity.Data.Models.Parametros;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.Helpers;
using Pixicity.Domain.ViewModels.Seguridad;
using System;
using System.Collections.Generic;
using static Pixicity.Domain.Enums.Enums;

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
        List<Usuario> GetUsuarios(QueryParamsHelper queryParameters, out long totalCount, bool isAdmin = false);

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
        /// Obtiene un usuario con el rango filtrado por el userName
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns></returns>
        Usuario GetUsuarioWithRangoByUserName(string userName);

        /// <summary>
        /// Obtiene un usuario con la información relacionada, filtrado por el userName
        /// </summary>
        /// <param name="userName">Nombre del usuario</param>
        /// <returns></returns>
        Usuario GetUsuarioInfoByUserName(string userName);

        /// <summary>
        /// Obtener la cantidad de comentarios realizados por el usuario
        /// </summary>
        /// <param name="userId">Id del usuario</param>
        /// <returns></returns>
        long CommentsCountByUserId(long userId);

        /// <summary>
        /// Obtener la cantidad de seguidores por usuario
        /// </summary>
        /// <param name="userId">Id del usuario</param>
        /// <returns></returns>
        int SeguidoresCountByUserId(long userId);

        /// <summary>
        /// Obtener la cantidad de usuarios que sigue
        /// </summary>
        /// <param name="userId">Id del usuario</param>
        /// <returns></returns>
        int SiguiendoCountByUserId(long userId);

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
        /// Obtener sesión de usuario por el id del usuario
        /// </summary>
        /// <param name="usuarioId">Id del usuario</param>
        /// <returns></returns>
        Session GetSessionByUsuarioId(long usuarioId);

        /// <summary>
        /// Actualizar el Campo de Activo de la sesión para determinar si se encuentra o no activo el Usuario
        /// </summary>
        /// <param name="model">Entidad Session</param>
        /// <returns></returns>
        DateTime UpdateSessionActivoDate(Session model);

        /// <summary>
        /// Obtiene la cantidad de usuarios online en la comunidad
        /// </summary>
        /// <returns></returns>
        long GetOnlineUsersCount();

        /// <summary>
        /// Guarda la sesión jwt en la base de datos
        /// </summary>
        /// <param name="model">Entidad Session</param>
        /// <returns></returns>
        long SaveUserSession(Session model);

        /// <summary>
        /// Eliminar todas las sesiones del usuario
        /// </summary>
        /// <param name="usuarioId">Id del Usuario</param>
        /// <returns></returns>
        long? DeleteAllSessionsByUsuarioId(long? usuarioId);

        /// <summary>
        /// Eliminar la sesión del usuario
        /// </summary>
        /// <param name="id">Id de la sesión</param>
        /// <returns></returns>
        bool DeleteSession(long id);

        /// <summary>
        /// Eliminar las sesiones de la IP
        /// </summary>
        /// <param name="IP">Dirección IP</param>
        /// <returns></returns>
        int DeleteSessionByIP(string IP);

        /// <summary>
        /// Obtener la información del usuario
        /// </summary>
        /// <param name="userName">Username</param>
        /// <returns></returns>
        UsuarioInfoViewModel GetUsuarioInfo(string userName);

        /// <summary>
        /// Seguir/unfollow un usuario
        /// </summary>
        /// <param name="model">Entidad UsuarioSeguidores</param>
        /// <returns></returns>
        long SeguirUsuario(UsuarioSeguidores model);

        /// <summary>
        /// Devuelve true si el usuario logeado sigue al usuario
        /// </summary>
        /// <param name="userName">Username del usuario</param>
        /// <returns></returns>
        bool IsFollowingTheUser(string userName);

        /// <summary>
        /// Obtener la lista de usuarios que está siguiendo el usuario
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<Usuario> GetFollowingUsersByUserId(QueryParamsHelper queryParameters, out long totalCount);

        /// <summary>
        /// Obtener la lista de usuarios que siguen al usuario id
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<Usuario> GetFollowersByUserId(QueryParamsHelper queryParameters, out long totalCount);

        List<Usuario> GetLastFollowersByUserId(long userId, out long totalCount);

        /// <summary>
        /// Obtener perfil del usuario actual
        /// </summary>
        /// <returns></returns>
        UsuarioPerfil GetCurrentPerfilInfo();

        /// <summary>
        /// Obtener el perfil del usuario por id
        /// </summary>
        /// <param name="usuarioId">Id del usuario</param>
        /// <returns></returns>
        UsuarioPerfil GetUsuarioPerfilByUsuarioId(long usuarioId);

        /// <summary>
        /// Guardar perfil del usuario
        /// </summary>
        /// <param name="model">UsuarioPerfil entidad</param>
        /// <returns></returns>
        long SavePerfilInfo(UsuarioPerfil model);

        /// <summary>
        /// Obtiene las redes sociales del usuario
        /// </summary>
        /// <param name="usuarioId">Id del usuario</param>
        /// <returns></returns>
        SocialMediaUsuarioPerfilViewModel GetSocialMediaByUsuarioId(long usuarioId);

        /// <summary>
        /// Obtiene la entidad Usuario descodificando el token jwt y obteniendo el userName de la propiedad unique_name
        /// </summary>
        /// <param name="jwt">JWT Token</param>
        /// <returns></returns>
        Usuario GetUsuarioByJWT(string jwt);

        /// <summary>
        /// Asignar última dirección IP al usuario
        /// </summary>
        /// <param name="usuario">Entidad de usuario</param>
        /// <param name="IP">Dirección IP</param>
        /// <returns></returns>
        long SetUltimaDireccionIPUsuario(Usuario usuario, string IP);

        /// <summary>
        /// Banear usuario
        /// </summary>
        /// <param name="model">Usuario</param>
        /// <returns></returns>
        long BanUser(Usuario model);

        /// <summary>
        /// Desbanear usuario
        /// </summary>
        /// <param name="model">Usuario</param>
        /// <returns></returns>
        long UnbanUser(Usuario model);

        string ChangeAvatar(string avatar);

        /// <summary>
        /// Obtener la lista de últimos usuarios registrados
        /// </summary>
        /// <returns></returns>
        List<Usuario> GetLastRegisteredUsers();

        /// <summary>
        /// Obtiene una lista de rango de usuarios
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<Rango> GetRangosUsuarios(QueryParamsHelper queryParameters, out long totalCount);

        /// <summary>
        /// Obtiene la lista completa de rangos
        /// </summary>
        /// <returns></returns>
        List<Rango> GetRangosDropdown();

        /// <summary>
        /// Crea una actividad del usuario
        /// </summary>
        /// <param name="model">Entidad Actividad</param>
        /// <returns></returns>
        long SaveActividadUsuario(Actividad model);

        ActividadLogsViewModel GetActividadesByUsuario(long usuarioId, TipoActividad? tipoActividad);

        RecordUsersOnlineViewModel GetRecordUsersOnline();

        /// <summary>
        /// Agregar o actualizar rango
        /// </summary>
        /// <param name="model">Entidad Rango</param>
        /// <returns></returns>
        long AddUpdateRango(Rango model);

        /// <summary>
        /// Actualizar el rango a un usuario
        /// </summary>
        /// <param name="model">Entidad Rango</param>
        /// <returns></returns>
        long ChangeRangoUsuario(RangoViewModel model);

        /// <summary>
        /// Agregar usuario a los usuarios en linea
        /// </summary>
        /// <param name="bearer">Bearer authorization</param>
        /// <returns></returns>
        int SessionOnlineUser(string bearer, string IP);

        /// <summary>
        /// Actualizar el fondo del perfil del usuario
        /// </summary>
        /// <param name="url">URL de la imagen</param>
        /// <returns></returns>
        string ChangeBackgroundProfile(string url);
    }
}
