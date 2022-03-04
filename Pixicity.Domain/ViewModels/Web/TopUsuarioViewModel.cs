using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Domain.ViewModels.Web
{
    public class TopUsuarioViewModel
    {
        public long Id { get; set; }
        public string Avatar { get; set; }
        public string UserName { get; set; }
        public int Puntos { get; set; }
    }
}
