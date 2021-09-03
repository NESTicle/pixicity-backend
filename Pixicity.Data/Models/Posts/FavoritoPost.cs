using Pixicity.Data.Models.Seguridad;
using System;

namespace Pixicity.Data.Models.Posts
{
    public class FavoritoPost
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public long PostId { get; set; }

        public DateTime FechaRegistro { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Post Post { get; set; }
    }
}
