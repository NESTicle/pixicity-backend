using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Data.Models.Posts
{
    public class Voto
    {
        public long Id { get; set; }
        public long TypeId { get; set; } // Puede ser el Id del Post o Fotos (en un futuro)
        public long UsuarioId { get; set; }
        public int Cantidad { get; set; }

        [Column("Tipo")]
        public string TipoString
        {
            get { return VotosType.ToString(); }
            private set { VotosType = value.ParseEnum<VotosTypeEnum>(); }
        }

        [NotMapped]
        public VotosTypeEnum VotosType { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        public Usuario Usuario { get; set; }
    }
}
