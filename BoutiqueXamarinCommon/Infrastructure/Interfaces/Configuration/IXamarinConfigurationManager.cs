using System;
using BoutiqueDTO.Infrastructure.Interfaces.Configuration;
using BoutiqueXamarinCommon.Models.Interfaces.Configuration;

namespace BoutiqueXamarinCommon.Infrastructure.Interfaces.Configuration
{
    /// <summary>
    /// Доступ к файлам конфигурации Xamarin
    /// </summary>
    public interface IXamarinConfigurationManager : IConfigurationManager<Guid, IXamarinConfigurationDomain>
    { }
}