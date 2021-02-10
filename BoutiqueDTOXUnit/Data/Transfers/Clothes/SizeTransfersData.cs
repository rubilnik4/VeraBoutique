using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTOXUnit.Data.Transfers.Clothes
{
    /// <summary>
    /// Размеры. Трансферные модели
    /// </summary>
    public static class SizeTransfersData
    {
        /// <summary>
        /// Размеры. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<SizeTransfer> SizeTransfers =>
            SizeData.SizeDomains.
            Select(size => new SizeTransfer(size)).
            ToList();
    }
}