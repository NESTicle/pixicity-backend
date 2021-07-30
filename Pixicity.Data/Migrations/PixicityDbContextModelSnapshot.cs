﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pixicity.Data.Models.Base;

namespace Pixicity.Data.Migrations
{
    [DbContext(typeof(PixicityDbContext))]
    partial class PixicityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Pixicity.Data.Models.Parametros.Estado", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Eliminado")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("FechaActualiza")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("FechaElimina")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("IdPais")
                        .HasColumnType("bigint");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("UsuarioActualiza")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioElimina")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioRegistra")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("IdPais");

                    b.ToTable("Estados", "Parametros");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Parametros.Pais", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Eliminado")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("FechaActualiza")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("FechaElimina")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ISO2")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)");

                    b.Property<string>("ISO3")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("UsuarioActualiza")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioElimina")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioRegistra")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Paises", "Parametros");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Parametros.Rango", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Eliminado")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("FechaActualiza")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("FechaElimina")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Icono")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("TipoString")
                        .HasColumnType("text")
                        .HasColumnName("Tipo");

                    b.Property<string>("UsuarioActualiza")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioElimina")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioRegistra")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Rangos", "Parametros");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Seguridad.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Baneado")
                        .HasColumnType("boolean");

                    b.Property<int>("Comentarios")
                        .HasColumnType("integer");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("EstadoId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("FechaActualiza")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("FechaElimina")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FechaNacimiento")
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("GeneroString")
                        .HasColumnType("text")
                        .HasColumnName("Genero");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Puntos")
                        .HasColumnType("integer");

                    b.Property<long>("RangoId")
                        .HasColumnType("bigint");

                    b.Property<int>("Seguidores")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UltimaConexion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UltimaIP")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("character varying(35)");

                    b.Property<string>("UsuarioActualiza")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioElimina")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioRegistra")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("RangoId");

                    b.ToTable("Usuarios", "Seguridad");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Web.Afiliado", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Banner")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("character varying(350)");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("FechaActualiza")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("FechaElimina")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("HitsIn")
                        .HasColumnType("integer");

                    b.Property<int>("HitsOut")
                        .HasColumnType("integer");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("character varying(350)");

                    b.Property<string>("UsuarioActualiza")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioElimina")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioRegistra")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Afiliados", "Web");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Parametros.Estado", b =>
                {
                    b.HasOne("Pixicity.Data.Models.Parametros.Pais", "Pais")
                        .WithMany("Estados")
                        .HasForeignKey("IdPais")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pais");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Seguridad.Usuario", b =>
                {
                    b.HasOne("Pixicity.Data.Models.Parametros.Estado", "Estado")
                        .WithMany("Usuarios")
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pixicity.Data.Models.Parametros.Rango", "Rango")
                        .WithMany("Usuarios")
                        .HasForeignKey("RangoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estado");

                    b.Navigation("Rango");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Parametros.Estado", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Parametros.Pais", b =>
                {
                    b.Navigation("Estados");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Parametros.Rango", b =>
                {
                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
