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
    }
}
