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
}
