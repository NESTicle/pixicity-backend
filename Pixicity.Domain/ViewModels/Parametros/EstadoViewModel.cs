using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Domain.ViewModels.Parametros
{
    public class EstadoViewModel
    {
        public string Nombre { get; set; }

        public virtual PaisViewModel Pais { get; set; }
    }
}
