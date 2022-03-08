using System;

namespace Pixicity.Data.Models.Web
{
    public class Configuracion
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string Slogan { get; set; }
        public string URL { get; set; }

        public DateTime DateCreated { get; set; }

        public bool MaintenanceMode { get; set; }
        public string MaintenanceMessage { get; set; }

        public bool DisableUserRegistration { get; set; }
        public string DisableUserRegistrationMessage { get; set; }

        public string OnlineUsersTime { get; set; }

        public string HeaderScript { get; set; }
        public string FooterScript { get; set; }

        public string Banner300x250 { get; set; }
        public string Banner468x60 { get; set; }
        public string Banner160x600 { get; set; }
        public string Banner728x90 { get; set; }

        public int RecordOnlineUsers { get; set; }
        public DateTime RecordOnlineTime { get; set; }
    }
}
