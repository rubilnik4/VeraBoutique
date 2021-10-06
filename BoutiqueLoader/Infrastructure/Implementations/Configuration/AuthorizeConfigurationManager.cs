﻿using System;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Models.Implementations.Configuration;
using BoutiqueDTO.Models.Implementations.Identities;
using BoutiqueLoader.Infrastructure.Interfaces.Configuration;
using BoutiqueLoader.Infrastructure.Interfaces.Converters;
using BoutiqueLoader.Models.Implementations.Configuration;
using BoutiqueLoader.Models.Interfaces.Configuration;

namespace BoutiqueLoader.Infrastructure.Implementations.Configuration
{
    /// <summary>
    /// Доступ к файлам авторизации консольного приложения
    /// </summary>
    public class AuthorizeConfigurationManager : ConsoleConfigurationManager<string, IAuthorizeDomain, AuthorizeTransfer>,
                                                 IAuthorizeConfigurationManager
    {
        public AuthorizeConfigurationManager(IAuthorizeTransferConverter authorizeTransferConverter)
          : base(authorizeTransferConverter)
        { }

        /// <summary>
        /// Имя файла конфигурации
        /// </summary>
        protected override string ConfigurationFileName => "loginsettings.json";
    }
}