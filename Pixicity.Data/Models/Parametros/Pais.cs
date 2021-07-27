using Pixicity.Data.Models.Base;
using System.Collections.Generic;

namespace Pixicity.Data.Models.Parametros
{
    public class Pais : PixicityBase
    {
        public string Nombre { get; set; }
        public string ISO2 { get; set; }
        public string ISO3 { get; set; }

        public virtual ICollection<Estado> Estados { get; set; } = new HashSet<Estado>();
    }
}
