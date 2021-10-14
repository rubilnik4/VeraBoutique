using System;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;

namespace BoutiqueConsole.Models.Interfaces.Configuration
{
    /// <summary>
    /// Конфигурация консольного загрузчика. Доменная модель
    /// </summary>
    public interface ILoaderConfigurationDomain : ILoaderConfigurationBase<IHostConfigurationDomain>, IDomainModel<Guid>
    { }
}