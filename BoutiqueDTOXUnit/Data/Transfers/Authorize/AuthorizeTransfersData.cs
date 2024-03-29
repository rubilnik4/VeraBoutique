﻿using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Identities;

namespace BoutiqueDTOXUnit.Data.Transfers.Authorize
{
    /// <summary>
    /// Имя пользователя и пароль. Трансферные модели
    /// </summary>
    public class AuthorizeTransfersData
    {
        /// <summary>
        /// Имя пользователя и пароль. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<AuthorizeTransfer> AuthorizeTransfers =>
            AuthorizeData.AuthorizeDomains.
            Select(authorize => new AuthorizeTransfer(authorize)).
            ToList();
    }
}