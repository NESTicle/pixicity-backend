using Pixicity.Data.Models.Base;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Data.Models.Web
{
    public class Visitas : PixicityBase
    {
        public long? UsuarioId { get; set; }
        public long TypeId { get; set; } // Id del post o de la foto

        [Column("Tipo")]
        public string TipoVisitasString
        {
            get { return TipoVisitas.ToString(); }
            private set { TipoVisitas = value.ParseEnum<VisitasTypeEnum>(); }
        }

        [NotMapped]
        public VisitasTypeEnum TipoVisitas { get; set; }

        public string IP { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
