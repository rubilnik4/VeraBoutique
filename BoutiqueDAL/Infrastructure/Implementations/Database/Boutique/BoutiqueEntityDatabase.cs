using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Configuration.Clothes;
using BoutiqueDAL.Factories.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.Models.Interfaces.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique
{
    /// <summary>
    /// База данных магазина Entity Framework
    /// </summary>
    public class BoutiqueEntityDatabase : EntityDatabase, IBoutiqueDatabase
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
        public IGenderDatabaseTable GendersTable =>
            new GenderDatabaseTable(Genders, nameof(Genders));

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public async Task UpdateSchema()
        {
          //  Database.EnsureDeleted();
            await  Database.EnsureCreatedAsync();
            await IdentityInitialize.Initialize(this);
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.HasPostgresEnum<GenderType>();

            InitializeEntityData(modelBuilder);
        }

        /// <summary>
        /// Инициализация данными таблиц
        /// </summary>
        private static void InitializeEntityData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenderEntity>().HasData(GenderInitialize.GenderData);
        }
    }
}