using Pixicity.Data.Models.Base;
using Pixicity.Data.Models.Logs;
using Pixicity.Data.Models.Parametros;
using Pixicity.Data.Models.Posts;
using Pixicity.Data.Models.Web;
using Pixicity.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Data.Models.Seguridad
{
    public class Usuario : PixicityBase
    {
        public long EstadoId { get; set; }
        public long RangoId { get; set; }

        public string Avatar { get; set; }
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

        public DateTime? UltimaConexion { get; set; }
        public string UltimaIP { get; set; }
        public bool Baneado { get; set; } = false;

        public virtual Estado Estado { get; set; }
        public virtual Rango Rango { get; set; }

        [NotMapped]
        public int CantidadPosts
        {
            get
            {
                if (Posts == null || Posts.Count <= 0)
                    return 0;

                return Posts.Count(x => x.Eliminado == false);
            }
        }

        [NotMapped]
        public int CantidadComentarios
        {
            get
            {
                if (Comentarios == null || Comentarios.Count <= 0)
                    return 0;

                return Comentarios.Count(x => x.Eliminado == false);
            }
        }

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
        public virtual ICollection<Comentario> Comentarios { get; set; } = new HashSet<Comentario>();
        public virtual ICollection<Voto> Votos { get; set; } = new HashSet<Voto>();
        public virtual ICollection<Denuncia> Denuncias { get; set; } = new HashSet<Denuncia>();
        public virtual ICollection<FavoritoPost> FavoritosPosts { get; set; } = new HashSet<FavoritoPost>();
        public virtual ICollection<Session> Sessions { get; set; } = new HashSet<Session>();
        public virtual ICollection<Monitor> Monitors { get; set; } = new HashSet<Monitor>();
        public virtual ICollection<Monitor> MonitorsUsuarioQueHaceAcciones { get; set; } = new HashSet<Monitor>();
        public virtual ICollection<UsuarioSeguidores> Seguidos { get; set; } = new HashSet<UsuarioSeguidores>();
        public virtual ICollection<UsuarioSeguidores> Seguidores { get; set; } = new HashSet<UsuarioSeguidores>();
        public virtual ICollection<UsuarioPerfil> UsuarioPerfil { get; set; } = new HashSet<UsuarioPerfil>();
        public virtual ICollection<SeguirPost> SeguirPosts { get; set; } = new HashSet<SeguirPost>();
        public virtual ICollection<Visitas> Visitas { get; set; } = new HashSet<Visitas>();
    }
}
