using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Common.Interfaces.Configuration;

namespace BoutiqueXamarin.Models.Interfaces.Configuration
{
    /// <summary>
    /// Конфигурация Xamarin
    /// </summary>
    public interface IXamarinConfigurationBase<out THost> : IModel<Guid>
        where THost: IHostConfigurationBase
    {
        /// <summary>
        /// Параметры подключения к серверу
        /// </summary>
        THost HostConfiguration { get; }
    }
}