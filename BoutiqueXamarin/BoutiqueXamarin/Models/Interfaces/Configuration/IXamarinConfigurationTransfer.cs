using System;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueDTO.Models.Implementations.Configuration;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueDTO.Models.Interfaces.Configuration;

namespace BoutiqueXamarin.Models.Interfaces.Configuration
{
    /// <summary>
    /// Конфигурация. Трансферная модель
    /// </summary>
    public interface IXamarinConfigurationTransfer : IXamarinConfigurationBase<HostConfigurationTransfer>, ITransferModel<Guid>
    { }
}