using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;

namespace BoutiqueDTOXUnit.Data.Transfers.Clothes
{
    /// <summary>
    /// Тип пола. Трансферные модели
    /// </summary>
    public static class GenderTransfersData
    {
        /// <summary>
        /// Тип пола. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<GenderTransfer> GenderTransfers =>
            GenderData.GenderDomains.
            Select(gender => new GenderTransfer(gender)).
            ToList();

        /// <summary>
        /// Тип пола с категорией. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<GenderCategoryTransfer> GenderCategoryTransfers =>
            GenderData.GenderCategoryDomains.
            Select(gender => new GenderCategoryTransfer(gender,
                                                       GetCategoryClothesTypeTransfers(gender.Categories))).
            ToList();

        /// <summary>
        /// Получить категории
        /// </summary>
        private static IEnumerable<CategoryClothesTypeTransfer> GetCategoryClothesTypeTransfers(IEnumerable<ICategoryClothesTypeDomain> categories) =>
            categories.Select(category => new CategoryClothesTypeTransfer(category,
                                                                          GetClothesTypeTransfers(category.ClothesTypes)));

        /// <summary>
        /// Получить типы одежды
        /// </summary>
        private static IEnumerable<ClothesTypeTransfer> GetClothesTypeTransfers(IEnumerable<IClothesTypeDomain> clothesTypes) =>
            clothesTypes.Select(clothesType => new ClothesTypeTransfer(clothesType));

    }
}