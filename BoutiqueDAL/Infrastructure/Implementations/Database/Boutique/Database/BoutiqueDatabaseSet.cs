using BoutiqueDAL.Infrastructure.Implementations.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Database
{
    /// <summary>
    /// База данных магазина Entity Framework
    /// </summary>
    public partial class BoutiqueEntityDatabase
    {
        /// <summary>
        /// Таблица пола базы данных
        /// </summary>
        public DbSet<GenderEntity> Genders { get; set; } = null!;

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        public DbSet<CategoryEntity> Categories { get; set; } = null!;

        /// <summary>
        /// Таблица базы данных цвета одежды
        /// </summary>
        public DbSet<ColorClothesEntity> ColorClothes { get; set; } = null!;

        /// <summary>
        /// Таблица базы данных вида одежды
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
        public DbSet<ClothesInformationEntity> Clothes { get; set; } = null!;
    }
}