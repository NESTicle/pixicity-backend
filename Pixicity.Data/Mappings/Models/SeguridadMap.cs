using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pixicity.Data.Models.Parametros;
using Pixicity.Data.Models.Seguridad;

namespace Pixicity.Data.Mappings.Models
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        void IEntityTypeConfiguration<Usuario>.Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios", "Seguridad");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.UserName)
                .HasMaxLength(35)
                .IsRequired();

            builder.Property(x => x.Password)
                .HasMaxLength(35);

            builder.Property(x => x.Email)
                .IsRequired();
        }
    }
}
