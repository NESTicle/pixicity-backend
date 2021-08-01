using Pixicity.Data.Models.Seguridad;
using System;

namespace Pixicity.Data.Models.Posts
{
    public class Comentario
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public long UsuarioId { get; set; }

        public string Contenido { get; set; }
        public DateTime FechaComentario { get; set; } = DateTime.Now;
        public int Votos { get; set; }
        public string IP { get; set; }
        
        public bool Eliminado { get; set; }

        public virtual Post Post { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
