using System.ComponentModel.DataAnnotations;

namespace Pixicity.Domain.Enums
{
    public class Enums
    {
        public enum GenerosEnum
        {
            [Display(Name = "Masculino")]
            Masculino = 1,
            [Display(Name = "Femenino")]
            Femenino = 2,
            [Display(Name = "Otros")]
            Otros = 3
        }

        public enum RangosEnum
        {
            [Display(Name = "Administrador")]
            Administrador = 1,
            [Display(Name = "Moderador")]
            Moderador = 2,
            [Display(Name = "Usuario")]
            Usuario = 3,
            [Display(Name = "Baneado")]
            Baneado = 666
        }

        public enum VotosTypeEnum
        {
            [Display(Name = "Posts")]
            Posts = 1,
            [Display(Name = "Fotos")]
            Fotos = 2
        }

        public enum VisitasTypeEnum
        {
            [Display(Name = "Posts")]
            Posts = 1,
            [Display(Name = "Fotos")]
            Fotos = 2
        }

        public enum TipoMonitor
        {
            [Display(Name = "Comentario")]
            Comentario = 1,
            [Display(Name = "Like")]
            Like = 2,
            [Display(Name = "Dislike")]
            Dislike = 3,
            [Display(Name = "Favoritos")]
            Favoritos = 4,
            [Display(Name = "Puntos")]
            Puntos = 5,
            [Display(Name = "Seguir")]
            Seguir = 6,
            [Display(Name = "ComentarioSiguePost")]
            ComentarioSiguePost = 7,
        }

        public enum TipoHistorial
        {
            [Display(Name = "Post")]
            Post = 1,
        }

        public enum TipoActividad
        {
            [Display(Name = "Post Nuevo")]
            PostNuevo = 1,
        }
    }
}
