using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroup;

namespace BoutiqueDTOXUnit.Data.Transfers
{
    /// <summary>
    /// Одежда. Трансферные модели
    /// </summary>
    public static class ClothesTransfersData
    {
        /// <summary>
        /// Типы одежды. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<ClothesTransfer> ClothesTransfers =>
            ClothesData.ClothesDomains.
            Select(clothes => 
                new ClothesTransfer(clothes, 
                                    new GenderTransfer(clothes.Gender), 
                                    new ClothesTypeShortTransfer(clothes.ClothesTypeShort, clothes.ClothesTypeShort.CategoryName),
                                    clothes.Colors.Select(color => new ColorClothesTransfer(color)),
                                    clothes.SizeGroups.Select(sizeGroup => new SizeGroupTransfer(sizeGroup, ToSizeTransfers(sizeGroup.Sizes))))).
            ToList();

        /// <summary>
        /// Получить трансферные модели размеров
        /// </summary>
        private static IEnumerable<SizeTransfer> ToSizeTransfers(IEnumerable<ISizeDomain> sizes) =>
            sizes.Select(size => new SizeTransfer(size));
    }
}