using Pixicity.Data.Models.Base;

namespace Pixicity.Data.Models.Web
{
    public class Afiliado : PixicityBase
    {
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string URL { get; set; }
        public string Banner { get; set; }
        public string Descripcion { get; set; }
        public int HitsIn { get; set; }
        public int HitsOut { get; set; }
    }
}
