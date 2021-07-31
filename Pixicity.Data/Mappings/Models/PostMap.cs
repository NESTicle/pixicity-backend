using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pixicity.Data.Models.Posts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Data.Mappings.Models
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        void IEntityTypeConfiguration<Post>.Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts", "Post");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.Titulo)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(x => x.Contenido)
                .IsRequired();

            builder.Property(x => x.Etiquetas)
                .IsRequired();

            builder.Property(x => x.IP)
                .HasMaxLength(15);
            
            builder.HasOne(x => x.Categoria)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.CategoriaId);

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.UsuarioId);
        }
    }
}
