﻿using Pixicity.Data.Models.Base;
using Pixicity.Data.Models.Parametros;
using Pixicity.Data.Models.Seguridad;

namespace Pixicity.Data.Models.Posts
{
    public class Borrador : PixicityBase
    {
        public long? CategoriaId { get; set; }
        public long? UsuarioId { get; set; }

        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string Etiquetas { get; set; }

        public bool EsPrivado { get; set; }
        public bool Smileys { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
