using Pixicity.Domain.ViewModels.Parametros;

namespace Pixicity.Domain.ViewModels.Seguridad
{
    public class UsuarioViewModel
    {
        public string Avatar { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long? PaisId { get; set; }
        public long EstadoId { get; set; }
        public int Genero { get; set; }
        public string Email { get; set; }
        public string FechaNacimiento { get; set; }

        public string GeneroString { get; set; }
        public EstadoViewModel Estado { get; set; }
    }
}
