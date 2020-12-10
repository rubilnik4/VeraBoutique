using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;

namespace BoutiqueDTOXUnit.Data.Transfers
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
            SizeData.SizeDomain.
            Select(size => new SizeTransfer(size)).
            ToList();
    }
}