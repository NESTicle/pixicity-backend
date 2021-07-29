using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.ViewModels.Seguridad;

namespace Pixicity.Service.Interfaces
{
    public interface ISeguridadService
    {
        /// <summary>
        /// Obtiene un usuario filtrado por el uerName
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns></returns>
        Usuario GetUsuarioByUserName(string userName);

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
    }
}
