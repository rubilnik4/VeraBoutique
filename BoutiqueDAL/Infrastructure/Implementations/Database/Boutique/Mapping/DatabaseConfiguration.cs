using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes.Composite;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Mapping
{
    /// <summary>
    /// Загрузка схем в базу данных
    /// </summary>
    public static class DatabaseConfiguration
    {
        /// <summary>
        /// Загрузить схемы перечислений в базу данных
        /// </summary>
        public static void ConfigureEnumsMapping()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<GenderType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<SizeType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<ClothesSizeType>();
        }

        /// <summary>
        /// Записать схемы базы данных
        /// </summary>
        public static void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            ClothesSequences.ApplyGenerator(modelBuilder);

            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ColorClothesConfiguration());
            modelBuilder.ApplyConfiguration(new ClothesTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClothesTypeGenderCompositeConfiguration());
            modelBuilder.ApplyConfiguration(new ClothesConfiguration());
            modelBuilder.ApplyConfiguration(new ClothesColorCompositeConfiguration());
            modelBuilder.ApplyConfiguration(new ClothesSizeGroupCompositeConfiguration());
            modelBuilder.ApplyConfiguration(new SizeConfiguration());
            modelBuilder.ApplyConfiguration(new SizeGroupConfiguration());
            modelBuilder.ApplyConfiguration(new SizeGroupCompositeConfiguration());

            modelBuilder.HasPostgresEnum<GenderType>();
            modelBuilder.HasPostgresEnum<SizeType>();
            modelBuilder.HasPostgresEnum<ClothesSizeType>();
        }

        /// <summary>
        /// Инициализация данными таблиц
        /// </summary> 
        public static void InitializeEntityData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenderEntity>().HasData(GenderInitialize.GenderData);
            modelBuilder.Entity<CategoryEntity>().HasData(CategoryInitialize.CategoryData);
            modelBuilder.Entity<ColorEntity>().HasData(ColorClothesInitialize.ColorClothesData);
            modelBuilder.Entity<ClothesTypeEntity>().HasData(ClothesTypeInitialize.ClothesTypeCategoryData);
            modelBuilder.Entity<ClothesTypeGenderCompositeEntity>().HasData(ClothesTypeInitialize.CompositeGenderData);
            modelBuilder.Entity<SizeEntity>().HasData(SizeInitialize.SizeData);
            modelBuilder.Entity<SizeGroupEntity>().HasData(SizeGroupInitialize.SizeGroupData);
            modelBuilder.Entity<SizeGroupCompositeEntity>().HasData(SizeGroupInitialize.CompositeSizeData);
        }
    }
}