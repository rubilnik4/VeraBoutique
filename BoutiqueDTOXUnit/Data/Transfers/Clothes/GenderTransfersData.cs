using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;

namespace BoutiqueDTOXUnit.Data.Transfers.Clothes
{
    /// <summary>
    /// Тип пола. Трансферные модели
    /// </summary>
    public static class GenderTransfersData
    {
        /// <summary>
        /// Тип пола. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<GenderTransfer> GenderTransfers =>
            GenderData.GenderDomains.
            Select(gender => new GenderTransfer(gender)).
            ToList();
    }
}