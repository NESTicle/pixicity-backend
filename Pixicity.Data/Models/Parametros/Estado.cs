using Pixicity.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Data.Models.Parametros
{
    public class Estado : PixicityBase
    {
        public long IdPais { get; set; }
        public string Nombre { get; set; }

        public virtual Pais Pais { get; set; }
    }
}
