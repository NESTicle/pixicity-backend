using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Domain.ViewModels.Posts
{
    public class FavoritosViewModel
    {
        public long Id { get; set; }
        public DateTime FechaRegistro { get; set; }

        public PostViewModel Post { get; set; }
    }
}
