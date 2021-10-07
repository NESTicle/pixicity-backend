using System;

namespace Pixicity.Domain.ViewModels.Seguridad
{
    public class SessionViewModel
    {
        public long Id { get; set; }
        public bool Eliminado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string UsuarioElimina { get; set; }
        public DateTime? FechaElimina { get; set; }

        public string Token { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public double DiffExpira
        {
            get { return Math.Round((FechaExpiracion - DateTime.Now).TotalDays); }
            set { }
        }

        public string Usuario { get; set; }
    }
}
