using Pixicity.Domain.ViewModels.Posts;
using System;

namespace Pixicity.Domain.ViewModels.Logs
{
    public class MonitorViewModel
    {
        public long Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Leido { get; set; }
        public bool Eliminado { get; set; }
        public string Mensaje { get; set; }
        public long? PostId { get; set; }
        public string TipoString { get; set; }
        
        public string UsuarioQueHaceAccion { get; set; }
        public PostViewModel Post { get; set; }
    }
}
