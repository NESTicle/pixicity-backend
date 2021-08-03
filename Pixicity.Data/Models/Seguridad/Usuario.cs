using Pixicity.Data.Models.Base;
using Pixicity.Data.Models.Parametros;
using Pixicity.Data.Models.Posts;
using Pixicity.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Data.Models.Seguridad
{
    public class Usuario : PixicityBase
    {
        public long EstadoId { get; set; }
        public long RangoId { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FechaNacimiento { get; set; }

        [Column("Genero")]
        public string GeneroString
        {
            get { return Genero.ToString(); }
            private set { Genero = value.ParseEnum<GenerosEnum>(); }
        }

        [NotMapped]
        public GenerosEnum Genero { get; set; }

        public int Puntos { get; set; }
        public int CantidadComentarios { get; set; }
        public int Seguidores { get; set; }

        public DateTime? UltimaConexion { get; set; }
        public string UltimaIP { get; set; }
        public bool Baneado { get; set; } = false;

        public virtual Estado Estado { get; set; }
        public virtual Rango Rango { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
        public virtual ICollection<Comentario> Comentarios { get; set; } = new HashSet<Comentario>();
        public virtual ICollection<Voto> Votos { get; set; } = new HashSet<Voto>();
    }
}
