using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;

namespace BoutiqueDTOXUnit.Data.Transfers.Clothes
{
    /// <summary>
    /// Одежда. Трансферные модели
    /// </summary>
    public static class ClothesTransfersData
    {
        /// <summary>
        /// Типы одежды. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<ClothesMainTransfer> ClothesTransfers =>
            ClothesData.ClothesDomains.
            Select(clothes => 
                new ClothesMainTransfer(clothes, 
                                    new GenderTransfer(clothes.Gender), 
                                    new ClothesTypeShortTransfer(clothes.ClothesTypeShort),
                                    clothes.Colors.Select(color => new ColorTransfer(color)),
                                    clothes.SizeGroups.Select(sizeGroup => new SizeGroupTransfer(sizeGroup, ToSizeTransfers(sizeGroup.Sizes))))).
            ToList();

        /// <summary>
        /// Типы одежды. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<ClothesTransfer> ClothesShortTransfers =>
            ClothesData.ClothesShortDomains.
            Select(clothesShort => new ClothesTransfer(clothesShort)).
            ToList();

        /// <summary>
        /// Получить трансферные модели размеров
        /// </summary>
        private static IEnumerable<SizeTransfer> ToSizeTransfers(IEnumerable<ISizeDomain> sizes) =>
            sizes.Select(size => new SizeTransfer(size));
    }
}