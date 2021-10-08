using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pixicity.Data.Models.Logs;

namespace Pixicity.Data.Mappings.Models
{
    public class LogsMap : IEntityTypeConfiguration<SystemLogs>
    {
        void IEntityTypeConfiguration<SystemLogs>.Configure(EntityTypeBuilder<SystemLogs> builder)
        {
            builder.ToTable("SystemLogs", "Logs");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.Response)
                .HasColumnType("jsonb");

            builder.Property(x => x.Data)
                .HasColumnType("jsonb");

            //builder.Property(x => x.StackTrace)
            //    .HasColumnType("jsonb");
        }
    }

    public class MonitorMap : IEntityTypeConfiguration<Monitor>
    {
        void IEntityTypeConfiguration<Monitor>.Configure(EntityTypeBuilder<Monitor> builder)
        {
            builder.ToTable("Monitores", "Logs");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Monitors)
                .HasForeignKey(x => x.UsuarioId);

            builder.HasOne(x => x.UsuarioQueHaceAccion)
                .WithMany(x => x.MonitorsUsuarioQueHaceAcciones)
                .HasForeignKey(x => x.UsuarioQueHaceAccionId);
        }
    }
}
