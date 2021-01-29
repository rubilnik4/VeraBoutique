using System;
using BoutiqueCommon.Models.Common.Interfaces.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Configuration
{
    /// <summary>
    /// Параметры подключения к серверу. Доменная модель
    /// </summary>
    public interface IHostConfigurationDomain: IHostConfigurationBase, IDomainModel<string>
    { }
}