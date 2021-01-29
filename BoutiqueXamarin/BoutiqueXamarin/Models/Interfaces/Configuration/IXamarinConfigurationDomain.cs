using System;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;

namespace BoutiqueXamarin.Models.Interfaces.Configuration
{
    /// <summary>
    /// Конфигурация. Доменная модель
    /// </summary>
    public interface IXamarinConfigurationDomain: IXamarinConfigurationBase<IHostConfigurationDomain>, IDomainModel<Guid>
    { }
}