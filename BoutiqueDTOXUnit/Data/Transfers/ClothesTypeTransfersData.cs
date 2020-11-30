using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroup;

namespace BoutiqueDTOXUnit.Data.Transfers
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
            ClothesTypeData.ClothesTypeDomain.
            Select(clothesType => new ClothesTypeTransfer(clothesType,
                                                          new CategoryTransfer(clothesType.Category),
                                                          clothesType.Genders.Select(gender => new GenderTransfer(gender)))).
            ToList();
    }
}