using System.Threading.Tasks;
using BoutiqueDAL.Configuration.Clothes;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Factories.Implementations.Database.Base;
using BoutiqueDAL.Factories.Interfaces.Database.Base;
using BoutiqueDAL.Factories.Interfaces.Database.Boutique;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Factories.Implementations.Database.Boutique
{
    /// <summary>
    /// База данных магазина Entity Framework
    /// </summary>
    public class BoutiqueEntityDatabase : DbContext, IBoutiqueDatabase
    {
        public BoutiqueEntityDatabase(DbContextOptions options)
            : base(options)
        { }

        /// <summary>
        /// Таблица пола базы данных EntityFramework
        /// </summary>
        public DbSet<GenderEntity> GendersSet { get; set; } = null!;

        /// <summary>
        /// Таблица пола базы данных
        /// </summary>
        public IDatabaseTable<GenderEntity> Genders => new EntityDatabaseTable<GenderEntity>(GendersSet);

        /// <summary>
        /// Сохранить изменения в базе асинхронно
        /// </summary>
        public async Task SaveChangesAsync() => await base.SaveChangesAsync();

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public void UpdateSchema() => base.Database.EnsureCreated();

        /// <summary>
        /// Записать схемы базы данных
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
        }
    }
}