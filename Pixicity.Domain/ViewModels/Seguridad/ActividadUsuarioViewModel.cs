using System;
using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Domain.ViewModels.Seguridad
{
    public class ActividadUsuarioViewModel
    {
        public long Id { get; set; }
        public DateTime FechaRegistro { get; set; }

        public string Avatar { get; set; }
        public string Texto { get; set; }
        public TipoActividad TipoActividad { get; set; }
    }
}
