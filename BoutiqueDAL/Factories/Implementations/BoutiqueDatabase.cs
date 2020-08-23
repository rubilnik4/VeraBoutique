using BoutiqueDAL.Configuration.Clothes;
using BoutiqueDAL.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BoutiqueDAL.Factories.Implementations
{
    public class BoutiqueDatabase : DbContext
    {
        public BoutiqueDatabase(DbContextOptions options)
            : base(options)
        { }

        public DbSet<GenderEntity> Genders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
        }
    }
}