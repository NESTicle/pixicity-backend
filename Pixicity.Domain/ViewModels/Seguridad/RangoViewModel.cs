using System;
using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Domain.ViewModels.Seguridad
{
    public class RangoViewModel
    {
        public long Id { get; set; }
        public DateTime FechaRegistro { get; set; }

        public string Icono { get; set; }
        public string Nombre { get; set; }
        public string TipoString { get; set; }
        public RangosEnum Tipo { get; set; }
        public string Color { get; set; }
        public int Puntos { get; set; }
        public int UsuariosCount { get; set; }

        public bool Eliminado { get; set; } = false;

        public string UsuarioRegistra { get; set; }
        public string UsuarioActualiza { get; set; }
        public DateTime? FechaActualiza { get; set; }

        public string UsuarioElimina { get; set; }
        public DateTime? FechaElimina { get; set; }
    }
}
