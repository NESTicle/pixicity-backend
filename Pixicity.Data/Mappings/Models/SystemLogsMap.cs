using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pixicity.Data.Models.Logs;

namespace Pixicity.Data.Mappings.Models
{
    public class SystemLogsMap : IEntityTypeConfiguration<SystemLogs>
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
}
