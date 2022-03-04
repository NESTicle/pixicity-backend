using System;
using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Domain.ViewModels.Seguridad
{
    public class RangoViewModel
    {
        public long Id { get; set; }
        public DateTime FechaRegistro { get; set; }

        public string Icono { get; set; }
        public string Nombre { get; set; }
        public string TipoString { get; set; }
        public RangosEnum Tipo { get; set; }
    }
}
