using Pixicity.Data.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pixicity.Data.Models.Seguridad
{
    public class UsuarioSeguidores : PixicityBase
    {
        public long SeguidoId { get; set; } // quien va a seguir
        public long SeguidorId { get; set; } // el usuario que va a seguir a seguidoId

        [NotMapped]
        public string UserName { get; set; }

        public virtual Usuario Seguido { get; set; }
        public virtual Usuario Seguidor { get; set; }
    }
}
