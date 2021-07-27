using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
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
        public static IReadOnlyCollection<ClothesMainTransfer> ClothesMainTransfers =>
            ClothesData.ClothesMainDomains.
            Select(clothes => 
                new ClothesMainTransfer(clothes,
                                        clothes.Images,
                                        new GenderTransfer(clothes.Gender), 
                                        new ClothesTypeTransfer(clothes.ClothesType),
                                        clothes.Colors.Select(color => new ColorTransfer(color)),
                                        clothes.SizeGroups.Select(sizeGroup => new SizeGroupMainTransfer(sizeGroup, ToSizeTransfers(sizeGroup.Sizes))))).
            ToList();

        /// <summary>
        /// Типы одежды. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<ClothesDetailTransfer> ClothesDetailTransfers =>
            ClothesData.ClothesDetailDomains.
            Select(clothes => 
                       new ClothesDetailTransfer(clothes,
                                                 clothes.Colors.Select(color => new ColorTransfer(color)),
                                                 clothes.SizeGroups.Select(sizeGroup => new SizeGroupMainTransfer(sizeGroup, ToSizeTransfers(sizeGroup.Sizes))))).
            ToList();
        
        /// <summary>
        /// Типы одежды. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<ClothesTransfer> ClothesTransfers =>
            ClothesData.ClothesDomains.
            Select(clothes => new ClothesTransfer(clothes)).
            ToList();

        /// <summary>
        /// Получить трансферные модели размеров
        /// </summary>
        private static IEnumerable<SizeTransfer> ToSizeTransfers(IEnumerable<ISizeDomain> sizes) =>
            sizes.Select(size => new SizeTransfer(size));
    }
}