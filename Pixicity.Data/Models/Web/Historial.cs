using Pixicity.Data.Models.Base;
using Pixicity.Domain.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Data.Models.Web
{
    public class Historial : PixicityBase
    {
        public long TipoId { get; set; }

        [Column("Tipo")]
        public string TipoString
        {
            get { return Tipo.ToString(); }
            private set { Tipo = value.ParseEnum<TipoHistorial>(); }
        }

        [NotMapped]
        public TipoHistorial Tipo { get; set; }

        public string Accion { get; set; }
        public string Razon { get; set; }
        public string IP { get; set; }
    }
}
