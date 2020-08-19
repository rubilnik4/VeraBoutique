using System;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueMVC.Models.Implementations.Clothes.Parameters
{
    /// <summary>
    /// Категории и тип одежды
    /// </summary>
    public class CategorizationClothes
    {
        public CategorizationClothes(GenderType genderType, string category, string subCategory)
        {
            if (String.IsNullOrWhiteSpace(category)) throw new ArgumentNullException(nameof(category));
            if (String.IsNullOrWhiteSpace(subCategory)) throw new ArgumentNullException(nameof(subCategory));

            GenderType = genderType;
            Category = category;
            SubCategory = subCategory;
        }

        /// <summary>
        /// Пол
        /// </summary>
        public GenderType GenderType { get; }

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
