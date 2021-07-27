using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Domain.ViewModels.Seguridad
{
    public class UsuarioViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public long EstadoId { get; set; }
        public int Genero { get; set; }
        public string Email { get; set; }
        public string FechaNacimiento { get; set; }
    }
}
