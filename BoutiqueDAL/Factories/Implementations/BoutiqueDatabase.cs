using BoutiqueDAL.Configuration.Clothes;
using BoutiqueDAL.Entities.Clothes;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Factories.Implementations
{
    public class BoutiqueDatabase : DbContext
    {
        public DbSet<GenderEntity> Genders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql("Host=postgres;Port=5432;Database=Boutique;Username=postgres;Password=postgres");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
        }
    }
}