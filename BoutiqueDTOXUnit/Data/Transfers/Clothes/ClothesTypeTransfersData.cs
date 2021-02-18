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
        public static IReadOnlyCollection<ClothesTypeTransfer> ClothesTypeTransfers =>
            ClothesTypeData.ClothesTypeMainDomains.
            Select(clothesType => new ClothesTypeTransfer(clothesType,
                                                          new CategoryTransfer(clothesType.Category),
                                                          clothesType.Genders.Select(gender => new GenderTransfer(gender)))).
            ToList();

        /// <summary>
        /// Типы одежды. Базовые данные. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeShortTransfer> ClothesTypeShortTransfers =>
            ClothesTypeData.ClothesTypeDomains.
            Select(clothesTypeShort => new ClothesTypeShortTransfer(clothesTypeShort)).
            ToList();
    }
}