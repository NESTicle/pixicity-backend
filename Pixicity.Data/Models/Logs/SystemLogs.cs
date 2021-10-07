using Pixicity.Domain.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pixicity.Data.Models.Logs
{
    public class SystemLogs
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Column("Status")]
        public string TypeString
        {
            get { return Type.ToString(); }
            private set { Type = value.ParseEnum<LogsType>(); }
        }

        [NotMapped]
        public LogsType Type { get; set; }

        public string Code { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Response { get; set; }

        public string URL { get; set; }
        public string Data { get; set; }
    }

    public enum LogsType
    {
        HTTP = 1,
        SYSTEM = 2,
        INFO = 99
    }
}
