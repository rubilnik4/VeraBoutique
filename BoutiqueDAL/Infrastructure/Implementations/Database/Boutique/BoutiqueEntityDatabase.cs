using System;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Configuration.Clothes;
using BoutiqueDAL.Factories.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.Models.Interfaces.Result;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultError.ResultErrorTryAsyncExtensions;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique
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
            new GenderDatabaseTable(Genders, nameof(Genders));

        /// <summary>
        /// Сохранить изменения в базе асинхронно
        /// </summary>
        public async Task<IResultError> SaveChangesAsync() => await ResultErrorTryAsync(()=> base.SaveChangesAsync(),
                                                                                        DatabaseErrors.DatabaseSaveError());

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public void UpdateSchema()
        {
            Database.EnsureCreated();
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