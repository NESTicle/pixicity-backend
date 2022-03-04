using Pixicity.Data.Models.Base;
using Pixicity.Domain.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Data.Models.Seguridad
{
    public class Actividad : PixicityBase
    {
        public long UsuarioId { get; set; }

        public long ObjId1 { get; set; }
        public long ObjId2 { get; set; }

        [Column("TipoActividad")]
        public string TipoActividadString
        {
            get { return TipoActividad.ToString(); }
            private set { TipoActividad = value.ParseEnum<TipoActividad>(); }
        }

        [NotMapped]
        public TipoActividad TipoActividad { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
