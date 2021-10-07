using Pixicity.Data.Models.Base;
using System;

namespace Pixicity.Data.Models.Seguridad
{
    public class Session : PixicityBase
    {
        public long UsuarioId { get; set; }
        public string Token { get; set; }
        public DateTime FechaExpiracion { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
