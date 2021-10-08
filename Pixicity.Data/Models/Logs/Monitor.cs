﻿using Pixicity.Data.Models.Seguridad;
using Pixicity.Domain.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using static Pixicity.Domain.Enums.Enums;

namespace Pixicity.Data.Models.Logs
{
    public class Monitor
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public long UsuarioQueHaceAccionId { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public bool Leido { get; set; }
        public string Mensaje { get; set; }

        [Column("Tipo")]
        public string TipoString
        {
            get { return Tipo.ToString(); }
            private set { Tipo = value.ParseEnum<TipoMonitor>(); }
        }

        [NotMapped]
        public TipoMonitor Tipo { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Usuario UsuarioQueHaceAccion { get; set; }
    }
}
