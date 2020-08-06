using System;
using VeraButik.Models.Enums;

namespace VeraButik.Models.Implementations.Clothes.Parameters
{
    /// <summary>
    /// Категории и тип одежды
    /// </summary>
    public class CategorisationClothes
    {
        public CategorisationClothes(SexType sexType, string category, string subCategory)
        {
            if (String.IsNullOrWhiteSpace(category)) throw new ArgumentNullException(nameof(category));
            if (String.IsNullOrWhiteSpace(subCategory)) throw new ArgumentNullException(nameof(subCategory));

            SexType = sexType;
            Category = category;
            SubCategory = subCategory;
        }

        /// <summary>
        /// Пол
        /// </summary>
        public SexType SexType { get; }

        /// <summary>
        /// Категория одежды
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// Подкатегория
        /// </summary>
        public string SubCategory { get; }
    }
}
