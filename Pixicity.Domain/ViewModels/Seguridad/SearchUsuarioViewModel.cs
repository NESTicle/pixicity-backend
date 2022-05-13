using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Domain.ViewModels.Seguridad
{
    public class SearchUsuarioViewModel
    {
        public bool? EnLinea { get; set; }
        public GenerosEnum? Genero { get; set; }
        public long? Pais { get; set; }
        public long? Rango { get; set; }
    }
}
