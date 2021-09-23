using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDTO.Models.Implementations.Identity;

namespace BoutiqueDTOXUnit.Data.Transfers.Authorize
{
    /// <summary>
    /// Личные данные. Трансферные модели
    /// </summary>
    public static class PersonalTransferData
    {
        /// <summary>
        /// Личные данные. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<PersonalTransfer> PersonalTransfers =>
            PersonalData.PersonalDomains.
            Select(personal => new PersonalTransfer(personal)).
            ToList();
    }
}