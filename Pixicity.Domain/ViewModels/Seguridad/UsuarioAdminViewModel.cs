using Pixicity.Domain.ViewModels.Parametros;
using System;

namespace Pixicity.Domain.ViewModels.Seguridad
{
    public class UsuarioAdminViewModel
    {
        public long Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Genero { get; set; }
        public int Puntos { get; set; }
        public DateTime? UltimaConexion { get; set; }
        public string UltimaIP { get; set; }
        public bool Baneado { get; set; }

        public int CantidadPosts { get; set; }
        public int CantidadComentarios { get; set; }

        public EstadoViewModel Estado { get; set; }
        public bool Eliminado { get; set; }
    }
}
