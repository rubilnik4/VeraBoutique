using System;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueXamarinCommon.Models.Implementation.Configuration;
using BoutiqueXamarinCommon.Models.Interfaces.Configuration;

namespace BoutiqueXamarinCommon.Infrastructure.Interfaces.Converters
{
    /// <summary>
    /// Конвертер конфигурации Xamarin  в трансферную модель
    /// </summary>
    public interface IXamarinConfigurationTransferConverter : ITransferConverter<Guid, IXamarinConfigurationDomain, XamarinConfigurationTransfer>
    { }
}