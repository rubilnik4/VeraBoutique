using BoutiqueEF.Entities.Clothes;
using BoutiqueEF.Mappings.Clothes;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueEF.Fabric
{
    public class BoutiqueDatabase : DbContext
    {
        public BoutiqueDatabase(DbContextOptions<BoutiqueDatabase> options) 
            : base(options)
        { }

        public DbSet<GenderEntity> Genders { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite(@"C:\Projects\Boutique.db");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
        }
    }
}