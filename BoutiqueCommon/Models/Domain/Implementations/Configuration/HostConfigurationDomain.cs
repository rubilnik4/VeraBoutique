using System;
using BoutiqueCommon.Models.Common.Implementations.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;

namespace BoutiqueCommon.Models.Domain.Implementations.Configuration
{
    /// <summary>
    /// Параметры подключения к серверу. Доменная модель
    /// </summary>
    public class HostConfigurationDomain: HostConfigurationBase, IHostConfigurationDomain
    {
        public HostConfigurationDomain(Uri host, TimeSpan timeOut)
            : base(host, timeOut)
        { }
    }
}