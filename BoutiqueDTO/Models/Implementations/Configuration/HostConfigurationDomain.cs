using System;
using BoutiqueCommon.Models.Common.Implementations.Configuration;
using BoutiqueDTO.Models.Interfaces.Configuration;

namespace BoutiqueDTO.Models.Implementations.Configuration
{
    /// <summary>
    /// Параметры подключения к серверу. Трансферная модель
    /// </summary>
    public class HostConfigurationTransfer: HostConfigurationBase, IHostConfigurationTransfer
    {
        public HostConfigurationTransfer(Uri host, TimeSpan timeOut)
            :base(host, timeOut)
        { }
    }
}