using System;
using BoutiqueConsole.Models.Interfaces.Configuration;
using BoutiqueDTO.Infrastructure.Interfaces.Configuration;

namespace BoutiqueConsole.Infrastructure.Interfaces.Configuration
{
    /// <summary>
    /// Доступ к файлам конфигурации консольного приложения
    /// </summary>
    public interface ILoaderConfigurationManager : IConfigurationManager<Guid, ILoaderConfigurationDomain>
    { }
}