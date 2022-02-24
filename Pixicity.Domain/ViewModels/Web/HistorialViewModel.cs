using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Domain.ViewModels.Web
{
    public class HistorialViewModel
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Accion { get; set; }
        public string Moderador { get; set; }
        public string Causa { get; set; }
    }
}
