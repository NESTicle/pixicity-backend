using Pixicity.Data.Models.Base;

namespace Pixicity.Data.Models.Parametros
{
    public class Pais : PixicityBase
    {
        public string Nombre { get; set; }
        public string ISO2 { get; set; }
        public string ISO3 { get; set; }
    }
}
