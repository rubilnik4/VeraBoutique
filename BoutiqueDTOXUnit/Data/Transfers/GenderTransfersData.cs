using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTOXUnit.Data.Transfers
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
            GenderData.GendersDomain.
            Select(gender => new GenderTransfer(gender)).
            ToList();
    }
}