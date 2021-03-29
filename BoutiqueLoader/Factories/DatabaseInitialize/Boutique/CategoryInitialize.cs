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
        public static IReadOnlyCollection<ICategoryMainDomain> Categories =>
            new List<ICategoryMainDomain>
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
        public static ICategoryMainDomain Outerwear =>
            new CategoryMainDomain("Верхняя одежда", GenderInitialize.Genders);

        /// <summary>
        /// Платья
        /// </summary>
        public static ICategoryMainDomain Dress =>
            new CategoryMainDomain("Платья", GenderInitialize.Female);

        /// <summary>
        /// Штаны
        /// </summary>
        public static ICategoryMainDomain Pants =>
            new CategoryMainDomain("Штаны", GenderInitialize.Genders);

        /// <summary>
        /// Обувь
        /// </summary>
        public static ICategoryMainDomain Shoes =>
            new CategoryMainDomain("Обувь", GenderInitialize.Genders);

        /// <summary>
        /// Аксессуары
        /// </summary>
        public static ICategoryMainDomain Accessories =>
            new CategoryMainDomain("Аксессуары", GenderInitialize.Genders);
    }
}