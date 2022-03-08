using Pixicity.Data.Models.Base;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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

        public string Color { get; set; }
        public int Puntos { get; set; }

        [NotMapped]
        public int UsuariosCount
        {
            get
            {
                if (Usuarios == null || Usuarios.Count <= 0)
                    return 0;

                return Usuarios.Where(x => x.Eliminado == false).Count();
            }
        }

        public virtual ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
    }
}
