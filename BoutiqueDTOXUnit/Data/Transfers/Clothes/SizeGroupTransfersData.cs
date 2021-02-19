using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;

namespace BoutiqueDTOXUnit.Data.Transfers.Clothes
{
    /// <summary>
    /// Группа размеров. Трансферные модели
    /// </summary>
    public static class SizeGroupTransfersData
    {
        /// <summary>
        /// Группа размеров. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<SizeGroupMainTransfer> SizeGroupMainTransfers =>
            SizeGroupData.SizeGroupMainDomains.
            Select(sizeGroupMain => new SizeGroupMainTransfer(sizeGroupMain,
                                                              sizeGroupMain.Sizes.Select(size => new SizeTransfer(size)))).
            ToList();

        /// <summary>
        /// Группа размеров. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<SizeGroupTransfer> SizeGroupTransfers =>
            SizeGroupData.SizeGroupDomains.
            Select(sizeGroup => new SizeGroupTransfer(sizeGroup)).
            ToList();
    }
}