using Microsoft.EntityFrameworkCore;
using Pixicity.Data.Mappings.Models;
using Pixicity.Data.Models.Logs;
using Pixicity.Data.Models.Parametros;
using Pixicity.Data.Models.Posts;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Data.Models.Web;

namespace Pixicity.Data
{
    public class PixicityDbContext : DbContext
    {
        public PixicityDbContext(DbContextOptions<PixicityDbContext> options) : base(options) { }

        #region Logs

        //public DbSet<SystemLogs> SystemLogs { get; set; }
        public DbSet<Monitor> Monitor { get; set; }

        #endregion

        #region Parametros

        public DbSet<Pais> Pais { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Rango> Rango { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

        #endregion

        #region Post

        public DbSet<Post> Post { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Voto> Voto { get; set; }
        public DbSet<FavoritoPost> FavoritoPost { get; set; }
        public DbSet<Borrador> Borradores { get; set; }
        public DbSet<SeguirPost> SeguirPost { get; set; }

        #endregion

        #region Seguridad

        public DbSet<Session> Session { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioSeguidores> UsuarioSeguidores { get; set; }
        public DbSet<UsuarioPerfil> UsuarioPerfil { get; set; }

        #endregion

        #region Web

        public DbSet<Afiliado> Afiliado { get; set; }
        public DbSet<Denuncia> Denuncia { get; set; }
        public DbSet<Configuracion> Configuracion { get; set; }
        public DbSet<Visitas> Visitas { get; set; }
        public DbSet<Historial> Historial { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PaisMap());
            builder.ApplyConfiguration(new EstadoMap());
            builder.ApplyConfiguration(new UsuarioMap());
            builder.ApplyConfiguration(new RangoMap());
            builder.ApplyConfiguration(new AfiliadoMap());
            builder.ApplyConfiguration(new CategoriaMap());
            builder.ApplyConfiguration(new PostMap());
            builder.ApplyConfiguration(new ComentarioMap());
            builder.ApplyConfiguration(new VotoMap());
            builder.ApplyConfiguration(new DenunciaMap());
            builder.ApplyConfiguration(new FavoritoPostMap());
            builder.ApplyConfiguration(new ConfiguracionMap());
            //builder.ApplyConfiguration(new SystemLogsMap());
            builder.ApplyConfiguration(new SessionMap());
            builder.ApplyConfiguration(new MonitorMap());
            builder.ApplyConfiguration(new UsuarioSeguidoresMap());
            builder.ApplyConfiguration(new UsuarioPerfilMap());
            builder.ApplyConfiguration(new BorradorMap());
            builder.ApplyConfiguration(new SeguirPostMap());
            builder.ApplyConfiguration(new VisitasMap());
            builder.ApplyConfiguration(new HistorialMap());
            
            base.OnModelCreating(builder);
        }
    }
}
