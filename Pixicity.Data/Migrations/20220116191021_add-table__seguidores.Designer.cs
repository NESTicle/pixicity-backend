﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pixicity.Data;

namespace Pixicity.Data.Migrations
{
    [DbContext(typeof(PixicityDbContext))]
    [Migration("20220116191021_add-table__seguidores")]
    partial class addtable__seguidores
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Pixicity.Data.Models.Logs.Monitor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Eliminado")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("FechaElimina")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Leido")
                        .HasColumnType("boolean");

                    b.Property<string>("Mensaje")
                        .HasColumnType("text");

                    b.Property<long?>("PostId")
                        .HasColumnType("bigint");

                    b.Property<string>("TipoString")
                        .HasColumnType("text")
                        .HasColumnName("Tipo");

                    b.Property<string>("UsuarioElimina")
                        .HasColumnType("text");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioQueHaceAccionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PostId");

                    b.HasIndex("UsuarioId");

                    b.HasIndex("UsuarioQueHaceAccionId");

                    b.ToTable("Monitores", "Logs");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Parametros.Categoria", b =>
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
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("SEO")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("UsuarioActualiza")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioElimina")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioRegistra")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Categorias", "Parametros");
                });

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

            modelBuilder.Entity("Pixicity.Data.Models.Posts.Comentario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("FechaComentario")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IP")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<int>("Votos")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PostId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Comentarios", "Post");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Posts.FavoritoPost", b =>
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

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<string>("UsuarioActualiza")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioElimina")
                        .HasColumnType("text");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<string>("UsuarioRegistra")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PostId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("FavoritosPost", "Post");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Posts.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CantidadComentarios")
                        .HasColumnType("integer");

                    b.Property<long>("CategoriaId")
                        .HasColumnType("bigint");

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("boolean");

                    b.Property<bool>("EsPrivado")
                        .HasColumnType("boolean");

                    b.Property<string>("Etiquetas")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Favoritos")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("FechaActualiza")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("FechaElimina")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IP")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<int>("Puntos")
                        .HasColumnType("integer");

                    b.Property<bool>("Smileys")
                        .HasColumnType("boolean");

                    b.Property<bool>("Sticky")
                        .HasColumnType("boolean");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("UsuarioActualiza")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioElimina")
                        .HasColumnType("text");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<string>("UsuarioRegistra")
                        .HasColumnType("text");

                    b.Property<int>("Visitantes")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UsuarioId");

                    b.ToTable("Posts", "Post");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Posts.Voto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("TipoString")
                        .HasColumnType("text")
                        .HasColumnName("Tipo");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UsuarioId");

                    b.ToTable("Votos", "Post");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Seguridad.Session", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Activo")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("FechaActualiza")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("FechaElimina")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaExpiracion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioActualiza")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioElimina")
                        .HasColumnType("text");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<string>("UsuarioRegistra")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UsuarioId");

                    b.ToTable("Sessions", "Seguridad");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Seguridad.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Baneado")
                        .HasColumnType("boolean");

                    b.Property<int>("CantidadComentarios")
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

            modelBuilder.Entity("Pixicity.Data.Models.Seguridad.UsuarioSeguidores", b =>
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

                    b.Property<long>("SeguidoId")
                        .HasColumnType("bigint");

                    b.Property<long>("SeguidorId")
                        .HasColumnType("bigint");

                    b.Property<string>("UsuarioActualiza")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioElimina")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioRegistra")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("SeguidoId");

                    b.HasIndex("SeguidorId");

                    b.ToTable("UsuarioSeguidores", "Seguridad");
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

            modelBuilder.Entity("Pixicity.Data.Models.Web.Configuracion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Banner160x600")
                        .HasColumnType("text");

                    b.Property<string>("Banner300x250")
                        .HasColumnType("text");

                    b.Property<string>("Banner468x60")
                        .HasColumnType("text");

                    b.Property<string>("Banner728x90")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FooterScript")
                        .HasColumnType("text");

                    b.Property<string>("HeaderScript")
                        .HasColumnType("text");

                    b.Property<string>("MaintenanceMessage")
                        .HasColumnType("text");

                    b.Property<bool>("MaintenanceMode")
                        .HasColumnType("boolean");

                    b.Property<string>("OnlineUsersTime")
                        .HasColumnType("text");

                    b.Property<string>("SiteName")
                        .HasColumnType("text");

                    b.Property<string>("Slogan")
                        .HasColumnType("text");

                    b.Property<string>("URL")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Configuracion", "Web");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Web.Denuncia", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Comentarios")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("character varying(800)");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("FechaActualiza")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("FechaElimina")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<long>("RazonDenunciaId")
                        .HasColumnType("bigint");

                    b.Property<string>("UsuarioActualiza")
                        .HasColumnType("text");

                    b.Property<long>("UsuarioDenunciaId")
                        .HasColumnType("bigint");

                    b.Property<string>("UsuarioElimina")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioRegistra")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PostId");

                    b.HasIndex("UsuarioDenunciaId");

                    b.ToTable("Denuncias", "Web");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Logs.Monitor", b =>
                {
                    b.HasOne("Pixicity.Data.Models.Posts.Post", "Post")
                        .WithMany("MonitorPosts")
                        .HasForeignKey("PostId");

                    b.HasOne("Pixicity.Data.Models.Seguridad.Usuario", "Usuario")
                        .WithMany("Monitors")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pixicity.Data.Models.Seguridad.Usuario", "UsuarioQueHaceAccion")
                        .WithMany("MonitorsUsuarioQueHaceAcciones")
                        .HasForeignKey("UsuarioQueHaceAccionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Usuario");

                    b.Navigation("UsuarioQueHaceAccion");
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

            modelBuilder.Entity("Pixicity.Data.Models.Posts.Comentario", b =>
                {
                    b.HasOne("Pixicity.Data.Models.Posts.Post", "Post")
                        .WithMany("Comentarios")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pixicity.Data.Models.Seguridad.Usuario", "Usuario")
                        .WithMany("Comentarios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Posts.FavoritoPost", b =>
                {
                    b.HasOne("Pixicity.Data.Models.Posts.Post", "Post")
                        .WithMany("FavoritosPosts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pixicity.Data.Models.Seguridad.Usuario", "Usuario")
                        .WithMany("FavoritosPosts")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Posts.Post", b =>
                {
                    b.HasOne("Pixicity.Data.Models.Parametros.Categoria", "Categoria")
                        .WithMany("Posts")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pixicity.Data.Models.Seguridad.Usuario", "Usuario")
                        .WithMany("Posts")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Posts.Voto", b =>
                {
                    b.HasOne("Pixicity.Data.Models.Seguridad.Usuario", "Usuario")
                        .WithMany("Votos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Seguridad.Session", b =>
                {
                    b.HasOne("Pixicity.Data.Models.Seguridad.Usuario", "Usuario")
                        .WithMany("Sessions")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
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

            modelBuilder.Entity("Pixicity.Data.Models.Seguridad.UsuarioSeguidores", b =>
                {
                    b.HasOne("Pixicity.Data.Models.Seguridad.Usuario", "Seguido")
                        .WithMany("Seguidos")
                        .HasForeignKey("SeguidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pixicity.Data.Models.Seguridad.Usuario", "Seguidor")
                        .WithMany("Seguidores")
                        .HasForeignKey("SeguidorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seguido");

                    b.Navigation("Seguidor");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Web.Denuncia", b =>
                {
                    b.HasOne("Pixicity.Data.Models.Posts.Post", "Post")
                        .WithMany("Denuncias")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pixicity.Data.Models.Seguridad.Usuario", "Usuario")
                        .WithMany("Denuncias")
                        .HasForeignKey("UsuarioDenunciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Parametros.Categoria", b =>
                {
                    b.Navigation("Posts");
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

            modelBuilder.Entity("Pixicity.Data.Models.Posts.Post", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("Denuncias");

                    b.Navigation("FavoritosPosts");

                    b.Navigation("MonitorPosts");
                });

            modelBuilder.Entity("Pixicity.Data.Models.Seguridad.Usuario", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("Denuncias");

                    b.Navigation("FavoritosPosts");

                    b.Navigation("Monitors");

                    b.Navigation("MonitorsUsuarioQueHaceAcciones");

                    b.Navigation("Posts");

                    b.Navigation("Seguidores");

                    b.Navigation("Seguidos");

                    b.Navigation("Sessions");

                    b.Navigation("Votos");
                });
#pragma warning restore 612, 618
        }
    }
}
