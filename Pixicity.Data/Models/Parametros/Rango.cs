using Pixicity.Data.Models.Base;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Data.Models.Parametros
{
    public class Rango : PixicityBase
    {
        public string Icono { get; set; }
        public string Nombre { get; set; }

        [Column("Tipo")]
        public string TipoString
        {
            get { return Tipo.ToString(); }
            private set { Tipo = value.ParseEnum<RangosEnum>(); }
        }

        [NotMapped]
        public RangosEnum Tipo { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
    }
}
