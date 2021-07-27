using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pixicity.Data.Models.Parametros;
using System;
using System.Collections.Generic;
using System.Text;

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

    public class EstadoMap : IEntityTypeConfiguration<Estado>
    {
        void IEntityTypeConfiguration<Estado>.Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("Estados", "Parametros");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.Nombre)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(x => x.Pais)
                .WithMany(x => x.Estados)
                .HasForeignKey(x => x.IdPais);
        }
    }

    public class RangoMap : IEntityTypeConfiguration<Rango>
    {
        void IEntityTypeConfiguration<Rango>.Configure(EntityTypeBuilder<Rango> builder)
        {
            builder.ToTable("Rangos", "Parametros");

            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.Nombre)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Icono)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
