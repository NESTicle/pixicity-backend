using Pixicity.Data.Models.Base;

namespace Pixicity.Data.Models.Parametros
{
    public class Categoria : PixicityBase
    {
        public string Nombre { get; set; }
        public string Slug { get; set; }
        public string Icono { get; set; }
    }
}
