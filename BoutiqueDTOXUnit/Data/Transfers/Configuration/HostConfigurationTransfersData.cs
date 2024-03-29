﻿using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueCommonXUnit.Data.Configuration;
using BoutiqueDTO.Models.Implementations.Configuration;

namespace BoutiqueDTOXUnit.Data.Transfers.Configuration
{
    /// <summary>
    /// Параметры авторизации. Трансферная модель
    /// </summary>
    public static class HostConfigurationTransfersData
    {
        /// <summary>
        /// Имя пользователя и пароль. Трансферные модели
        /// </summary>
        public static IReadOnlyCollection<HostConfigurationTransfer> HostConfigurationTransfers =>
            HostConfigurationData.HostConfigurationDomains.
            Select(hostConfiguration => new HostConfigurationTransfer(hostConfiguration)).
            ToList();
    }
}