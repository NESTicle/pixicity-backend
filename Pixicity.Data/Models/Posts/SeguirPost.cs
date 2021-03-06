using Pixicity.Data.Models.Seguridad;
using System;

namespace Pixicity.Data.Models.Posts
{
    public class SeguirPost
    {
        public long Id { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public bool Eliminado { get; set; } = false;

        public long UsuarioId { get; set; }
        public long PostId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Post Post { get; set; }
    }
}
