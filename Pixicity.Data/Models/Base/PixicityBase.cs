using System;
using System.ComponentModel.DataAnnotations;

namespace Pixicity.Data.Models.Base
{
    public class PixicityBase
    {
        [Key]
        public long Id { get; set; }

        public bool Eliminado { get; set; } = false;

        public string UsuarioRegistra { get; set; } = "Invitado";
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public string UsuarioActualiza { get; set; } = "";
        public DateTime? FechaActualiza { get; set; } = null;

        public string UsuarioElimina { get; set; } = "";
        public DateTime? FechaElimina { get; set; } = null;
    }
}
