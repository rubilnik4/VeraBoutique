using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTOXUnit.Data.Transfers.Clothes
{
    /// <summary>
    /// Цвет одежды. Трансферные модели
    /// </summary>
    public class ColorTransfersData
    {
        /// <summary>
        /// Цвет одежды. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<ColorTransfer> ColorTransfers =>
            ColorData.ColorDomain.
            Select(color => new ColorTransfer(color)).
            ToList();
    }
}