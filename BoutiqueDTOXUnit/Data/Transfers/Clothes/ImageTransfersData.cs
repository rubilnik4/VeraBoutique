using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ImageTransfers;

namespace BoutiqueDTOXUnit.Data.Transfers.Clothes
{
    /// <summary>
    /// Изображения. Трансферные модели
    /// </summary>
    public class ImageTransfersData
    {
        /// <summary>
        /// Изображения. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<ClothesImageTransfer> ClothesImageTransfers =>
            ImageData.ClothesImageDomains.
            Select(image => new ClothesImageTransfer(image)).
            ToList();
    }
}