using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Common.Interfaces.Configuration;

namespace BoutiqueXamarinCommon.Models.Interfaces.Configuration
{
    /// <summary>
    /// Конфигурация Xamarin
    /// </summary>
    public interface IXamarinConfigurationBase<THost> : IModel<Guid>, IEquatable<IXamarinConfigurationBase<THost>>
        where THost: IHostConfigurationBase
    {
        /// <summary>
        /// Параметры подключения к серверу
        /// </summary>
        THost HostConfiguration { get; }
    }
}