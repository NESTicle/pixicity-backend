using System.ComponentModel.DataAnnotations;

namespace Pixicity.Domain.Enums
{
    public class Enums
    {
        public enum GenerosEnum
        {
            [Display(Name = "Masculino")]
            Masculino = 1,
            [Display(Name = "Femenino")]
            Femenino = 2,
            [Display(Name = "Otros")]
            Otros = 3
        }

        public enum RangosEnum
        {
            [Display(Name = "Administrador")]
            Administrador = 1,
            [Display(Name = "Moderador")]
            Moderador = 2,
            [Display(Name = "Usuario")]
            Usuario = 3,
            [Display(Name = "Usuario")]
            Baneado = 666
        }
    }
}
