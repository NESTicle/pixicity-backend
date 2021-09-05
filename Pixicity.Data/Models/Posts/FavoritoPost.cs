using Pixicity.Data.Models.Base;
using Pixicity.Data.Models.Seguridad;

namespace Pixicity.Data.Models.Posts
{
    public class FavoritoPost : PixicityBase
    {
        public long UsuarioId { get; set; }
        public long PostId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Post Post { get; set; }
    }
}
