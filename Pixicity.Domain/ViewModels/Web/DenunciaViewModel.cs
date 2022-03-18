using Pixicity.Domain.ViewModels.Posts;
using System;

namespace Pixicity.Domain.ViewModels.Web
{
    public class DenunciaViewModel
    {
        public long Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public long RazonDenunciaId { get; set; }

        public string Comentarios { get; set; }

        public PostViewModel Post { get; set; }
        public string UsuarioDenuncia { get; set; }
    }
}
