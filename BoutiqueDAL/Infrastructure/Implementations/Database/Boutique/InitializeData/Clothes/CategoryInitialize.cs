using System.Collections.Generic;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes
{
    /// <summary>
    /// Начальные данные таблицы категорий одежды
    /// </summary>
    public class CategoryInitialize
    {
        /// <summary>
        /// Начальные данные таблицы категорий одежды
        /// </summary>
        public static IReadOnlyCollection<CategoryEntity> CategoryData =>
            new List<CategoryEntity>
            {
                new CategoryEntity(Outerwear),
                new CategoryEntity(Dress),
                new CategoryEntity(Pants),
                new CategoryEntity(Shoes),
                new CategoryEntity(Accessories),
            }.AsReadOnly();

        /// <summary>
        /// Верхняя одежда
        /// </summary>
        public static string Outerwear => "Верхняя одежда";

        /// <summary>
        /// Платья
        /// </summary>
        public static string Dress => "Платья";

        /// <summary>
        /// Штаны
        /// </summary>
        public static string Pants => "Штаны";

        /// <summary>
        /// Обувь
        /// </summary>
        public static string Shoes => "Обувь";

        /// <summary>
        /// Аксессуары
        /// </summary>
        public static string Accessories => "Аксессуары";
    }
}