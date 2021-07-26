using Pixicity.Data.Models.Base;
using System;

namespace Pixicity.Data.Models.Seguridad
{
    public class Usuario : PixicityBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FechaNacimiento { get; set; }
        
        public int Rango { get; set; } // rango del usuario (admin, mod, user, etc)
        public int Puntos { get; set; }
        public int Comentarios { get; set; }
        public int Seguidores { get; set; }
        public int PuntosPorDar { get; set; }

        public DateTime SiguientesPuntos { get; set; }

        public DateTime UltimaConexion { get; set; }
        public string UltimaIP { get; set; }

        public int CambiosDelUserName { get; set; } = 0;
        public bool Baneado { get; set; } = false;
    }
}
