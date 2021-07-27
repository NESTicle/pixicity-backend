using Pixicity.Data.Models.Base;
using Pixicity.Data.Models.Seguridad;
using System.Collections.Generic;

namespace Pixicity.Data.Models.Parametros
{
    public class Estado : PixicityBase
    {
        public long IdPais { get; set; }
        public string Nombre { get; set; }

        public virtual Pais Pais { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
    }
}
