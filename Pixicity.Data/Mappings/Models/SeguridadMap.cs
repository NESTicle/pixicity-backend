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
                .IsRequired();

            builder.Property(x => x.Email)
                .IsRequired();

            builder.HasOne(x => x.Rango)
                .WithMany(x => x.Usuarios)
                .HasForeignKey(x => x.RangoId);
        }
    }

    public class SessionMap : IEntityTypeConfiguration<Session>
    {
        void IEntityTypeConfiguration<Session>.Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Sessions", "Seguridad");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.UsuarioId);
        }
    }

    public class UsuarioSeguidoresMap : IEntityTypeConfiguration<UsuarioSeguidores>
    {
        void IEntityTypeConfiguration<UsuarioSeguidores>.Configure(EntityTypeBuilder<UsuarioSeguidores> builder)
        {
            builder.ToTable("UsuarioSeguidores", "Seguridad");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.HasOne(x => x.Seguido)
                .WithMany(x => x.Seguidos)
                .HasForeignKey(x => x.SeguidoId);

            builder.HasOne(x => x.Seguidor)
                .WithMany(x => x.Seguidores)
                .HasForeignKey(x => x.SeguidorId);
        }
    }
}
