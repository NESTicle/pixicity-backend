using Pixicity.Domain.ViewModels.Parametros;

namespace Pixicity.Domain.ViewModels.Posts
{
    public class PostViewModel
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string Etiquetas { get; set; }

        public int Puntos { get; set; }
        public int Comentarios { get; set; }
        public int Favoritos { get; set; }
        public int Visitantes { get; set; }

        public bool Sticky { get; set; }
        public bool Smileys { get; set; }

        public CategoriaViewModel Categoria { get; set; }
    }
}
