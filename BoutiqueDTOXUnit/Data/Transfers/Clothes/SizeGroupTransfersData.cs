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
        public static IReadOnlyCollection<SizeGroupMainTransfer> SizeGroupTransfers =>
            SizeGroupData.SizeGroupDomains.
            Select(sizeGroup => new SizeGroupTransfer(sizeGroup,
                                                      sizeGroup.Sizes.Select(size => new SizeTransfer(size)))).
            ToList();

        /// <summary>
        /// Группа размеров. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<SizeGroupShortTransfer> SizeGroupShortTransfers =>
            SizeGroupData.SizeGroupShortDomains.
            Select(sizeGroupShort => new SizeGroupShortTransfer(sizeGroupShort)).
            ToList();
    }
}