using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Configuration.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
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
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ClothesTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClothesTypeGenderConfiguration());
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
            modelBuilder.Entity<ClothesTypeEntity>().HasData(ClothesTypeInitialize.ClothesTypeData);
            modelBuilder.Entity<ClothesTypeGenderEntity>().HasData(ClothesTypeGenderInitialize.ClothesTypeGenderData);
            modelBuilder.Entity<SizeGroupEntity>().HasData(SizeGroupInitialize.SizeGroupData);
            modelBuilder.Entity<SizeEntity>().HasData(SizeInitialize.SizeData);
        }
    }
}