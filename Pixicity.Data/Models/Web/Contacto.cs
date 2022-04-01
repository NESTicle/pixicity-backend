using System;
using System.ComponentModel.DataAnnotations;

namespace Pixicity.Data.Models.Web
{
    public class Contacto
    {
        [Key]
        public long Id { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Medio { get; set; }
        public string Comentarios { get; set; }
    }
}
