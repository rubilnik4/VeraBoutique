using System;
using BoutiqueCommon.Models.Common.Implementations.Configuration;
using BoutiqueCommon.Models.Common.Interfaces.Configuration;
using BoutiqueDTO.Models.Interfaces.Configuration;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Configuration
{
    /// <summary>
    /// Параметры подключения к серверу. Трансферная модель
    /// </summary>
    public class HostConfigurationTransfer: HostConfigurationBase, IHostConfigurationTransfer
    {
        public HostConfigurationTransfer(IHostConfigurationBase hostConfiguration)
          : this(hostConfiguration.Host, hostConfiguration.TimeOut, hostConfiguration.DisableSSL)
        { }

        [JsonConstructor]
        public HostConfigurationTransfer(Uri host, string timeOut, bool disableSSL)
           : this(host, TimeSpan.Parse(timeOut), disableSSL)
        { }

        public HostConfigurationTransfer(Uri host, TimeSpan timeOut, bool disableSSL)
            :base(host, timeOut, disableSSL)
        { }
    }
}