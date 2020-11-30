using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroup;

namespace BoutiqueDTOXUnit.Data.Transfers
{
    /// <summary>
    /// Группа размеров. Трансферные модели
    /// </summary>
    public static class SizeGroupTransfersData
    {
        /// <summary>
        /// Группа размеров. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<SizeGroupTransfer> SizeGroupTransfers =>
            SizeGroupData.SizeGroupDomain.
            Select(sizeGroup => new SizeGroupTransfer(sizeGroup,
                                                      sizeGroup.Sizes.Select(size => new SizeTransfer(size)))).
            ToList();
    }
}