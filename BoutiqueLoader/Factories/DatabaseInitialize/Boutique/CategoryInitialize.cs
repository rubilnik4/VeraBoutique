using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;

namespace BoutiqueLoader.Factories.DatabaseInitialize.Boutique
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
            new List<ICategoryDomain>
            {
                Outerwear,
                Dress,
                Pants,
                Shoes,
                Accessories,
            }.AsReadOnly();

        /// <summary>
        /// Верхняя одежда
        /// </summary>
        public static ICategoryDomain Outerwear =>
            new CategoryDomain("Верхняя одежда");

        /// <summary>
        /// Платья
        /// </summary>
        public static ICategoryDomain Dress =>
            new CategoryDomain("Платья");

        /// <summary>
        /// Штаны
        /// </summary>
        public static ICategoryDomain Pants =>
            new CategoryDomain("Штаны");

        /// <summary>
        /// Обувь
        /// </summary>
        public static ICategoryDomain Shoes =>
            new CategoryDomain("Обувь");

        /// <summary>
        /// Аксессуары
        /// </summary>
        public static ICategoryDomain Accessories =>
            new CategoryDomain("Аксессуары");
    }
}