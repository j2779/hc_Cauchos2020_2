using System.Data.Entity;
using Utilitarios;

namespace Datos
{
    public class Mapeo : DbContext
    {
        static Mapeo()
        {
            Database.SetInitializer<Mapeo>(null);
        }
        private readonly string schema;
        public Mapeo()
            : base("Name = Conexion")
        {

        }


        public DbSet<UEncapUsuario> usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(this.schema);
            base.OnModelCreating(modelBuilder);
        }
    }
}
