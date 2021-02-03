using System;
using BoutiqueDTO.Infrastructure.Interfaces.Configuration;
using BoutiqueLoader.Models.Interfaces.Configuration;

namespace BoutiqueLoader.Infrastructure.Interfaces.Configuration
{
    /// <summary>
    /// Доступ к файлам конфигурации консольного приложения
    /// </summary>
    public interface ILoaderConfigurationManager : IConfigurationManager<Guid, ILoaderConfigurationDomain>
    { }
}