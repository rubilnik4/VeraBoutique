using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Configuration.Clothes;
using BoutiqueDAL.Factories.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultError.ResultErrorTryAsyncExtensions;

namespace BoutiqueDALXUnit.Data.Database
{
    /// <summary>
    /// Тестовая база данных
    /// </summary>
    public class TestEntityDatabase : EntityDatabase, IDatabase
    {
        public TestEntityDatabase(DbContextOptions options)
          : base(options)
        { }

        /// <summary>
        /// Тестовая таблица EntityFramework
        /// </summary>
        public DbSet<TestEntity> Test { get; set; } = null!;

        /// <summary>
        /// Таблица пола базы данных
        /// </summary>
        public IDatabaseTable<TestEnum, TestEntity> TestTable =>
            new TestDatabaseTable(Test, nameof(Test));

        /// <summary>
        /// Записать параметры конфигурации
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder builder) =>
            NpgsqlConnection.GlobalTypeMapper.MapEnum<TestEnum>();

        /// <summary>
        /// Записать схемы базы данных
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TestConfiguration());
            modelBuilder.HasPostgresEnum<TestEnum>();
        }

        /// <summary>
        /// Обновить схему
        /// </summary>
        public void UpdateSchema()
        { }
    }
}