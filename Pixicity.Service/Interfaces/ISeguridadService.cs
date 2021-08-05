using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.ViewModels.Seguridad;

namespace Pixicity.Service.Interfaces
{
    public interface ISeguridadService
    {
        /// <summary>
        /// Obtiene la cantidad de Usuarios registrados en el sistema
        /// </summary>
        /// <returns></returns>
        long CountUsuarios();

        /// <summary>
        /// Obtiene un usuario filtrado por el uerName
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns></returns>
        Usuario GetUsuarioByUserName(string userName);

        /// <summary>
        /// Obtiene un usuario filtrado por el Id
        /// </summary>
        /// <param name="id">Id del Usuario</param>
        /// <returns></returns>
        Usuario GetUsuarioById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        long RegistrarUsuario(UsuarioViewModel usuario);

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
    }
}
