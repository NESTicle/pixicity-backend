﻿using Pixicity.Domain.ViewModels.Parametros;
using System;

namespace Pixicity.Domain.ViewModels.Seguridad
{
    public class PerfilUsuarioViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Puntos { get; set; }
        public long ComentariosCount { get; set; }
        public int PostsCount { get; set; }
        public int SeguidoresCount { get; set; }
        public int SiguiendoCount { get; set; }

        public PaisViewModel Pais { get; set; }
    }
}
