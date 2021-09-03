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
                .HasMaxLength(100)
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

    public class ComentarioMap : IEntityTypeConfiguration<Comentario>
    {
        void IEntityTypeConfiguration<Comentario>.Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("Comentarios", "Post");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.Contenido)
                .IsRequired();

            builder.Property(x => x.IP)
                .HasMaxLength(15);

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Comentarios)
                .HasForeignKey(x => x.UsuarioId);

            builder.HasOne(x => x.Post)
                .WithMany(x => x.Comentarios)
                .HasForeignKey(x => x.PostId);
        }
    }

    public class VotoMap : IEntityTypeConfiguration<Voto>
    {
        void IEntityTypeConfiguration<Voto>.Configure(EntityTypeBuilder<Voto> builder)
        {
            builder.ToTable("Votos", "Post");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Votos)
                .HasForeignKey(x => x.UsuarioId);
        }
    }

    public class FavoritoPostMap : IEntityTypeConfiguration<FavoritoPost>
    {
        void IEntityTypeConfiguration<FavoritoPost>.Configure(EntityTypeBuilder<FavoritoPost> builder)
        {
            builder.ToTable("FavoritosPost", "Post");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.FavoritosPosts)
                .HasForeignKey(x => x.UsuarioId);

            builder.HasOne(x => x.Post)
                .WithMany(x => x.FavoritosPosts)
                .HasForeignKey(x => x.PostId);
        }
    }
}
