using System;
using BoutiqueConsole.Models.Implementations.Configuration;
using BoutiqueConsole.Models.Interfaces.Configuration;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;

namespace BoutiqueConsole.Infrastructure.Interfaces.Converters
{
    /// <summary>
    /// Конвертер конфигурации консольного загрузчика в трансферную модель
    /// </summary>
    public interface ILoaderConfigurationTransferConverter : ITransferConverter<Guid, ILoaderConfigurationDomain, LoaderConfigurationTransfer>
    { }
}