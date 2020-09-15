using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BoutiqueDALXUnit.Data.Database.Implementation
{
    /// <summary>
    /// Тестовая база данных
    /// </summary>
    public class TestEntityDatabase : EntityDatabase, ITestDatabase
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
        public ITestDatabaseTable TestTable =>
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