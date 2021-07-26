using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pixicity.Data.Models.Parametros;

namespace Pixicity.Data.Mappings.Models
{
    public class PaisMap : IEntityTypeConfiguration<Pais>
    {
        void IEntityTypeConfiguration<Pais>.Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("Paises", "Parametros");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.ISO2)
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(x => x.ISO3)
                .HasMaxLength(3)
                .IsRequired();
        }
    }
}
