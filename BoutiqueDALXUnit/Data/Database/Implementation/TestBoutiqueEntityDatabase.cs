using System;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Identity;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Mapping;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Implementations.Identity;
using Functional.FunctionalExtensions.Async;
using Functional.Models.Interfaces.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BoutiqueDALXUnit.Data.Database.Implementation
{
    public class TestBoutiqueEntityDatabase: EntityDatabase, IBoutiqueDatabase
    {
        public TestBoutiqueEntityDatabase(DbContextOptions options)
         : base(options)
        { }

        /// <summary>
        /// Таблица пола базы данных EntityFramework
        /// </summary>
        public DbSet<GenderEntity> Genders { get; set; } = null!;

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        public DbSet<CategoryEntity> Category { get; set; } = null!;

        /// <summary>
        /// Таблица базы данных цвета одежды
        /// </summary>
        public DbSet<ColorEntity> ColorClothes { get; set; } = null!;

        /// <summary>
        /// Таблица базы данных вида одежды EntityFramework
        /// </summary>
        public DbSet<ClothesTypeEntity> ClothesTypes { get; set; } = null!;

        /// <summary>
        /// Таблица базы данных размеров одежды
        /// </summary>
        public DbSet<SizeEntity> Sizes { get; set; } = null!;

        /// <summary>
        /// Таблица базы данных группы размеров одежды
        /// </summary>
        public DbSet<SizeGroupEntity> SizeGroups { get; set; } = null!;

        /// <summary>
        /// Таблица базы данных  одежды
        /// </summary>
        public DbSet<ClothesEntity> Clothes { get; set; } = null!;

        /// <summary>
        /// Таблица базы данных размеров одежды
        /// </summary>
        public ISizeTable SizeTable => new SizeTable(Sizes);

        /// <summary>
        /// Таблица базы данных группы размеров одежды
        /// </summary>
        public ISizeGroupTable SizeGroupTable => new SizeGroupTable(SizeGroups);

        /// <summary>
        /// Таблица базы данных цвета одежды
        /// </summary>
        public IColorClothesTable ColorClothesTable => new ColorClothesTable(ColorClothes);

        /// <summary>
        /// Таблица пола базы данных
        /// </summary>
        public IGenderTable GendersTable => new GenderTable(Genders);

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        public ICategoryTable CategoryTable => new CategoryTable(Category);

        /// <summary>
        /// Таблица базы данных вида одежды
        /// </summary>
        public IClothesTypeTable ClotheTypeTable => new ClothesTypeTable(ClothesTypes);

        /// <summary>
        /// Таблица базы данных одежды
        /// </summary>
        public IClothesTable ClothesTable => new ClothesTable(Clothes);

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public async Task UpdateSchema()
        {
            await Database.EnsureDeletedAsync();
            await Database.EnsureCreatedAsync();
        }

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public async Task UpdateSchema(UserManager<IdentityUser> userManager, IResultCollection<BoutiqueUser> defaultUsers) =>
            await Task.FromResult(userManager);

        /// <summary>
        /// Записать параметры конфигурации
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder builder) =>
            DatabaseConfiguration.ConfigureEnumsMapping();

        /// <summary>
        /// Записать схемы базы данных
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            DatabaseConfiguration.ApplyConfiguration(modelBuilder);
        }
    }
}