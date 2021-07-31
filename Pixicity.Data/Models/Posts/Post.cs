using Pixicity.Data.Models.Base;
using Pixicity.Data.Models.Parametros;
using Pixicity.Data.Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Data.Models.Posts
{
    public class Post : PixicityBase
    {
        public long CategoriaId { get; set; }
        public long UsuarioId { get; set; }

        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string Etiquetas { get; set; }
        
        public int Puntos { get; set; }
        public int Comentarios { get; set; }
        public int Favoritos { get; set; }
        public int Visitantes { get; set; }

        public string IP { get; set; }
        public bool EsPrivado { get; set; }
        public bool Sticky { get; set; }
        public bool Smileys { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
