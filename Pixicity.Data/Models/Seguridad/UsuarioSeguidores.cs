using Pixicity.Data.Models.Base;

namespace Pixicity.Data.Models.Seguridad
{
    public class UsuarioSeguidores : PixicityBase
    {
        public long SeguidoId { get; set; }
        public long SeguidorId { get; set; }

        public virtual Usuario Seguido { get; set; }
        public virtual Usuario Seguidor { get; set; }
    }
}
