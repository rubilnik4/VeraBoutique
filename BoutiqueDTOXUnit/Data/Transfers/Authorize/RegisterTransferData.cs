using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDTO.Models.Implementations.Identity;

namespace BoutiqueDTOXUnit.Data.Transfers.Authorize
{
    /// <summary>
    /// Регистрация. Трансферные модели
    /// </summary>
    public static class RegisterTransferData
    {
        /// <summary>
        /// Регистрация. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<RegisterTransfer> RegisterTransfers =>
            RegisterData.RegisterDomains.
            Select(register => new RegisterTransfer(new AuthorizeTransfer(register.Authorize),
                                                    new PersonalTransfer(register.Personal))).
            ToList();
    }
}