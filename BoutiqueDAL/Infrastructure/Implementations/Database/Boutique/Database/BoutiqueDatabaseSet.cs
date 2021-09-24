using BoutiqueDAL.Infrastructure.Implementations.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Database
{
    /// <summary>
    /// База данных магазина Entity Framework
    /// </summary>
    public partial class BoutiqueDatabase
    {
        /// <summary>
        /// Таблица пола базы данных
        /// </summary>
        public DbSet<GenderEntity> Genders { get; init; } = null!;

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        public DbSet<CategoryEntity> Categories { get; init; } = null!;

        /// <summary>
        /// Таблица базы данных цвета одежды
        /// </summary>
        public DbSet<ColorEntity> ColorClothes { get; init; } = null!;

        /// <summary>
        /// Таблица базы данных цвета одежды
        /// </summary>
        public DbSet<ClothesImageEntity> ClothesImages { get; init; } = null!;

        /// <summary>
        /// Таблица базы данных вида одежды
        /// </summary>
        public DbSet<ClothesTypeEntity> ClothesTypes { get; init; } = null!;

        /// <summary>
        /// Таблица базы данных размеров одежды
        /// </summary>
        public DbSet<SizeEntity> Sizes { get; init; } = null!;

        /// <summary>
        /// Таблица базы данных группы размеров одежды
        /// </summary>
        public DbSet<SizeGroupEntity> SizeGroups { get; init; } = null!;

        /// <summary>
        /// Таблица базы данных  одежды
        /// </summary>
        public DbSet<ClothesEntity> Clothes { get; init; } = null!;
    }
}