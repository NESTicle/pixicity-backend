using Pixicity.Domain.ViewModels.Parametros;

namespace Pixicity.Domain.ViewModels.Web
{
    public class TopPostsViewModel
    {
        public long Id { get; set; }
        public string URL { get; set; }
        public string Titulo { get; set; }
        public int Puntos { get; set; }

        public CategoriaViewModel Categoria { get; set; }
    }
}
