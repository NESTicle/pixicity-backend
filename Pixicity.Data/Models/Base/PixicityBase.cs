using System;
using System.ComponentModel.DataAnnotations;

namespace Pixicity.Data.Models.Base
{
    public class PixicityBase
    {
        [Key]
        public long Id { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
