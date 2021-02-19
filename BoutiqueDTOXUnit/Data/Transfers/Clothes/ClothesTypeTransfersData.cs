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
    /// Типы одежды. Трансферные модели
    /// </summary>
    public static class ClothesTypeTransfersData
    {
        /// <summary>
        /// Типы одежды. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeMainTransfer> ClothesTypeMainTransfers =>
            ClothesTypeData.ClothesTypeMainDomains.
            Select(clothesTypeMain => new ClothesTypeMainTransfer(clothesTypeMain,
                                                                  new CategoryTransfer(clothesTypeMain.Category))).
            ToList();

        /// <summary>
        /// Типы одежды. Базовые данные. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeTransfer> ClothesTypeTransfers =>
            ClothesTypeData.ClothesTypeDomains.
            Select(clothesTypeShort => new ClothesTypeTransfer(clothesTypeShort)).
            ToList();
    }
}