using Pixicity.Domain.ViewModels.Parametros;
using System;

namespace Pixicity.Domain.ViewModels.Posts
{
    public class PostViewModel
    {
        public long Id { get; set; }
        public string URL { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string Etiquetas { get; set; }

        public int Puntos { get; set; }
        public int CantidadComentarios { get; set; }
        public int Favoritos { get; set; }
        public int Visitantes { get; set; }

        public bool Sticky { get; set; }
        public bool Smileys { get; set; }

        public DateTime FechaRegistro { get; set; }

        public CategoriaViewModel Categoria { get; set; }

        #region Parámetros de búsqueda

        public string Search { get; set; }
        public string SearchType { get; set; }

        #endregion
    }
}
