using System;

namespace Pixicity.Domain.ViewModels.Posts
{
    public class ComentarioViewModel
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public long UsuarioId { get; set; }

        public string Contenido { get; set; }
        public DateTime FechaComentario { get; set; }
        public int Votos { get; set; }
        public string IP { get; set; }

        public bool Eliminado { get; set; }

        public PostViewModel Post { get; set; }
        public string Usuario { get; set; }
    }
}
