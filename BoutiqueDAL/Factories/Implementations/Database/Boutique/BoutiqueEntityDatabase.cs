using System.Reflection.Emit;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Configuration.Clothes;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Factories.Implementations.Database.Base;
using BoutiqueDAL.Factories.Interfaces.Database.Base;
using BoutiqueDAL.Factories.Interfaces.Database.Boutique;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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
        public DbSet<GenderEntity> Genders { get; set; } = null!;

        /// <summary>
        /// Таблица пола базы данных
        /// </summary>
        public IDatabaseTable<GenderType, GenderEntity> GendersTable => 
            new EntityDatabaseTable<GenderType, GenderEntity>(Genders, nameof(Genders));

        /// <summary>
        /// Сохранить изменения в базе асинхронно
        /// </summary>
        public async Task SaveChangesAsync() => await base.SaveChangesAsync();

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public void UpdateSchema()
        {
            //base.Database.EnsureDeleted();
            base.Database.EnsureCreated();
        }

        /// <summary>
        /// Записать параметры конфигурации
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder builder) =>
            NpgsqlConnection.GlobalTypeMapper.MapEnum<GenderType>();

        /// <summary>
        /// Записать схемы базы данных
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.HasPostgresEnum<GenderType>();
        }
    }
}