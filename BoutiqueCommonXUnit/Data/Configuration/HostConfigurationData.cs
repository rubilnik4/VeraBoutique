using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Configuration;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;

namespace BoutiqueCommonXUnit.Data.Configuration
{
    /// <summary>
    /// Данные параметров авторизации
    /// </summary>
    public class HostConfigurationData
    {
        /// <summary>
        /// Получить параметры авторизации
        /// </summary>
        public static IReadOnlyCollection<IHostConfigurationDomain> HostConfigurationDomains =>
            new List<IHostConfigurationDomain>
            {
                new HostConfigurationDomain(new Uri("https://localhost:5001"), TimeSpan.FromSeconds(5), false),
                new HostConfigurationDomain(new Uri("https://localhost:443"), TimeSpan.FromSeconds(2), true),
            };
    }
}