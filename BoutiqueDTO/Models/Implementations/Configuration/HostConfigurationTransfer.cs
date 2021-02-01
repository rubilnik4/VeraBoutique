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
          : this(hostConfiguration.Host, hostConfiguration.TimeOut)
        { }

        [JsonConstructor]
        public HostConfigurationTransfer(Uri host, int timeOut)
           : this(host, TimeSpan.FromSeconds(timeOut))
        { }

        public HostConfigurationTransfer(Uri host, TimeSpan timeOut)
            :base(host, timeOut)
        { }
    }
}