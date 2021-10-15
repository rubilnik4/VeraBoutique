using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDTO.Models.Implementations.Identities;

namespace BoutiqueDTOXUnit.Data.Transfers.Authorize
{
    /// <summary>
    /// Данные пользователей. Трансферные модели
    /// </summary>
    public class IdentityTransfersData
    {
        /// <summary>
        /// Данные пользователей. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<BoutiqueUserTransfer> BoutiqueUserTransfers =>
            IdentityData.BoutiqueUsers.
            Select(user => new BoutiqueUserTransfer(user)).
            ToList();
    }
}