using Microsoft.EntityFrameworkCore;
using Pixicity.Data.Mappings.Models;
using Pixicity.Data.Models.Parametros;
using Pixicity.Data.Models.Posts;
using Pixicity.Data.Models.Seguridad;
using Pixicity.Data.Models.Web;

namespace Pixicity.Data
{
    public class PixicityDbContext : DbContext
    {
        public PixicityDbContext(DbContextOptions<PixicityDbContext> options) : base(options) { }

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

        #endregion

        #region Seguridad

        public DbSet<Usuario> Usuario { get; set; }

        #endregion

        #region Web

        public DbSet<Afiliado> Afiliado { get; set; }
        public DbSet<Denuncia> Denuncia { get; set; }

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
            
            base.OnModelCreating(builder);
        }
    }
}
