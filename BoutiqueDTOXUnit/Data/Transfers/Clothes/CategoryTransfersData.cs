using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;

namespace BoutiqueDTOXUnit.Data.Transfers.Clothes
{
    /// <summary>
    /// Категория одежды. Трансферные модели
    /// </summary>
    public static class CategoryTransfersData
    {
        /// <summary>
        /// Категория одежды. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<CategoryMainTransfer> CategoryMainTransfers =>
            CategoryData.CategoryMainDomains.
            Select(category => new CategoryMainTransfer(category,
                                                        category.Genders.Select(gender => new GenderTransfer(gender)))).
            ToList();

        /// <summary>
        /// Категория одежды с типом. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<CategoryClothesTypeTransfer> CategoryClothesTypeTransfers =>
            CategoryData.CategoryClothesTypeDomains.
            Select(category => new CategoryClothesTypeTransfer(category,
                                                               category.ClothesTypes.Select(clothesType => new ClothesTypeTransfer(clothesType)))).
            ToList();

        /// <summary>
        /// Категория одежды. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<CategoryTransfer> CategoryTransfers =>
            CategoryData.CategoryDomains.
            Select(category => new CategoryTransfer(category)).
            ToList();
    }
}