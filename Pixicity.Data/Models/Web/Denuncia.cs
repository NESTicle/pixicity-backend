using Pixicity.Data.Models.Base;
using Pixicity.Data.Models.Posts;
using Pixicity.Data.Models.Seguridad;

namespace Pixicity.Data.Models.Web
{
    public class Denuncia : PixicityBase
    {
        public long PostId { get; set; }
        public long UsuarioDenunciaId { get; set; }
        public long RazonDenunciaId { get; set; }

        public string Comentarios { get; set; }

        public virtual Post Post { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
