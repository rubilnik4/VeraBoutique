using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;

namespace BoutiquePrerequisites.Factories.DatabaseInitialize.Boutique
{
    /// <summary>
    /// Начальные данные таблицы категорий одежды
    /// </summary>
    public class CategoryInitialize
    {
        /// <summary>
        /// Начальные данные таблицы категорий одежды
        /// </summary>
        public static IReadOnlyCollection<ICategoryDomain> Categories =>
            new List<CategoryDomain>
            {
                new (Outerwear),
                new (Dress),
                new (Pants),
                new (Shoes),
                new (Accessories),
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