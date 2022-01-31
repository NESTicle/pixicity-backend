using Pixicity.Data.Models.Base;
using Pixicity.Data.Models.Posts;
using System.Collections.Generic;

namespace Pixicity.Data.Models.Parametros
{
    public class Categoria : PixicityBase
    {
        public string Nombre { get; set; }
        public string SEO { get; set; }
        public string Icono { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
        public virtual ICollection<Borrador> Borradores { get; set; } = new HashSet<Borrador>();
    }
}
