using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pixicity.Data.Models.Web;

namespace Pixicity.Data.Mappings.Models
{
    public class AfiliadoMap : IEntityTypeConfiguration<Afiliado>
    {
        void IEntityTypeConfiguration<Afiliado>.Configure(EntityTypeBuilder<Afiliado> builder)
        {
            builder.ToTable("Afiliados", "Web");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.Codigo)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Titulo)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.URL)
                .HasMaxLength(350)
                .IsRequired();

            builder.Property(x => x.Banner)
                .HasMaxLength(350)
                .IsRequired();

            builder.Property(x => x.Descripcion)
                .HasMaxLength(200)
                .IsRequired();
        }
    }

    public class DenunciaMap : IEntityTypeConfiguration<Denuncia>
    {
        void IEntityTypeConfiguration<Denuncia>.Configure(EntityTypeBuilder<Denuncia> builder)
        {
            builder.ToTable("Denuncias", "Web");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.Comentarios)
                .HasMaxLength(800)
                .IsRequired();

            builder.HasOne(x => x.Post)
                .WithMany(x => x.Denuncias)
                .HasForeignKey(x => x.PostId);

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Denuncias)
                .HasForeignKey(x => x.UsuarioDenunciaId);
        }
    }

    public class ConfiguracionMap : IEntityTypeConfiguration<Configuracion>
    {
        void IEntityTypeConfiguration<Configuracion>.Configure(EntityTypeBuilder<Configuracion> builder)
        {
            builder.ToTable("Configuracion", "Web");

            builder.HasIndex(x => x.Id).IsUnique();
        }
    }

    public class VisitasMap : IEntityTypeConfiguration<Visitas>
    {
        void IEntityTypeConfiguration<Visitas>.Configure(EntityTypeBuilder<Visitas> builder)
        {
            builder.ToTable("Visitas", "Web");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.IP)
                .HasMaxLength(15)
                .IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Visitas)
                .HasForeignKey(x => x.UsuarioId);
        }
    }

    public class HistorialMap : IEntityTypeConfiguration<Historial>
    {
        void IEntityTypeConfiguration<Historial>.Configure(EntityTypeBuilder<Historial> builder)
        {
            builder.ToTable("Historial", "Web");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.Accion)
                .IsRequired();

            builder.Property(x => x.Razon)
                .IsRequired();

            builder.Property(x => x.IP)
                .HasMaxLength(15)
                .IsRequired();
        }
    }

    public class ContactoMap : IEntityTypeConfiguration<Contacto>
    {
        void IEntityTypeConfiguration<Contacto>.Configure(EntityTypeBuilder<Contacto> builder)
        {
            builder.ToTable("Contacto", "Web");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.Nombre)
                .IsRequired();

            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.Comentarios)
                .IsRequired();
        }
    }
}
