using Microsoft.EntityFrameworkCore;
using Pixicity.Data.Mappings.Models;
using Pixicity.Data.Models.Parametros;

namespace Pixicity.Data.Models.Base
{
    public class PixicityDbContext : DbContext
    {
        public PixicityDbContext(DbContextOptions<PixicityDbContext> options) : base(options) { }

        public DbSet<Pais> Pais { get; set; }
        public DbSet<Estado> Estado { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PaisMap());
            builder.ApplyConfiguration(new EstadoMap());

            base.OnModelCreating(builder);
        }
    }
}
