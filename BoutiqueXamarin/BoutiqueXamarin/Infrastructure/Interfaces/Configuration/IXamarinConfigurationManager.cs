using System;
using BoutiqueXamarin.Models.Interfaces.Configuration;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Configuration
{
    /// <summary>
    /// Доступ к файлам конфигурации Xamarin
    /// </summary>
    public interface IXamarinConfigurationManager : IConfigurationManager<Guid, IXamarinConfigurationDomain>
    { }
}