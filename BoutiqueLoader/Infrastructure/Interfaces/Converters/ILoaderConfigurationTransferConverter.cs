using System;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueLoader.Models.Implementations.Configuration;
using BoutiqueLoader.Models.Interfaces.Configuration;

namespace BoutiqueLoader.Infrastructure.Interfaces.Converters
{
    /// <summary>
    /// Конвертер конфигурации консольного загрузчика в трансферную модель
    /// </summary>
    public interface ILoaderConfigurationTransferConverter : ITransferConverter<Guid, ILoaderConfigurationDomain, LoaderConfigurationTransfer>
    { }
}