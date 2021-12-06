using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Carts;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Configuration.Clothes.Composite;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Mapping.Sequences;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
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
            DatabaseSequence.ClothesSequence.ApplyGenerator(modelBuilder);

            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ColorClothesConfiguration());
            modelBuilder.ApplyConfiguration(new ClothesTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GenderCategoryCompositeConfiguration());
            modelBuilder.ApplyConfiguration(new ClothesImageConfiguration());
            modelBuilder.ApplyConfiguration(new ClothesConfiguration());
            modelBuilder.ApplyConfiguration(new ClothesColorCompositeConfiguration());
            modelBuilder.ApplyConfiguration(new ClothesSizeGroupCompositeConfiguration());
            modelBuilder.ApplyConfiguration(new SizeConfiguration());
            modelBuilder.ApplyConfiguration(new SizeGroupConfiguration());
            modelBuilder.ApplyConfiguration(new SizeGroupCompositeConfiguration());

            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());

            modelBuilder.HasPostgresEnum<GenderType>();
            modelBuilder.HasPostgresEnum<SizeType>();
            modelBuilder.HasPostgresEnum<ClothesSizeType>();
        }
    }
}