using Microsoft.EntityFrameworkCore;
using SistemaLogin.Data.Map;
using SistemaLogin.Models;

namespace SistemaLogin.Data
{
    public class SistemaLoginDBContext : DbContext // Herda
    {
        public SistemaLoginDBContext(DbContextOptions<SistemaLoginDBContext> options) : base(options) { }

        public DbSet<UserModel> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
