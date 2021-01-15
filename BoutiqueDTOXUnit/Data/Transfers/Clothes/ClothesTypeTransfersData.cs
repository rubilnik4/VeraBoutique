using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;

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
            ClothesTypeData.ClothesTypeDomains.
            Select(clothesType => new ClothesTypeTransfer(clothesType,
                                                          new CategoryTransfer(clothesType.Category),
                                                          clothesType.Genders.Select(gender => new GenderTransfer(gender)))).
            ToList();

        /// <summary>
        /// Типы одежды. Базовые данные. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeShortTransfer> ClothesTypeShortTransfers =>
            ClothesTypeData.ClothesTypeShortDomains.
            Select(clothesTypeShort => new ClothesTypeShortTransfer(clothesTypeShort)).
            ToList();
    }
}