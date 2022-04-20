using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Domain.ViewModels.Seguridad
{
    public class UsuarioPerfilViewModel
    {
        public long UsuarioId { get; set; }

        public string CompleteName { get; set; }
        public string PersonalMessage { get; set; }
        public string Website { get; set; }

        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Tiktok { get; set; }
        public string Youtube { get; set; }

        public bool Like1 { get; set; }
        public bool Like2 { get; set; }
        public bool Like3 { get; set; }
        public bool Like4 { get; set; }
        public bool Like_All { get; set; }

        public string EstadoCivil { get; set; }
        public string Hijos { get; set; }
        public string VivoCon { get; set; }
        public string Altura { get; set; }
        public string Peso { get; set; }
        public string ColorCabello { get; set; }
        public string ColorOjos { get; set; }
        public string Complexion { get; set; }
        public string Dieta { get; set; }
        public bool Tatuajes { get; set; }
        public bool Piercings { get; set; }
        public string Fumo { get; set; }
        public string Alcohol { get; set; }

        public string Estudios { get; set; }
        public string Profesion { get; set; }
        public string Empresa { get; set; }
        public string Sector { get; set; }
        public string InteresesProfesionales { get; set; }
        public string HabilidadesProfesionales { get; set; }

        public string MisIntereses { get; set; }
        public string Hobbies { get; set; }
        public string SeriesTV { get; set; }
        public string MusicaFavorita { get; set; }
        public string DeportesFavoritos { get; set; }
        public string LibrosFavoritos { get; set; }
        public string PeliculasFavoritas { get; set; }
        public string ComidaFavorita { get; set; }
        public string MisHeroesSon { get; set; }

        public virtual UsuarioViewModel Usuario { get; set; }
    }
}
