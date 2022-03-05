using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Domain.ViewModels.Seguridad
{
    public class ActividadLogsViewModel
    {
        public List<ActividadUsuarioViewModel> Hoy { get; set; }
        public List<ActividadUsuarioViewModel> Ayer { get; set; }
        public List<ActividadUsuarioViewModel> Semana { get; set; }
        public List<ActividadUsuarioViewModel> Mes { get; set; }
    }
}
