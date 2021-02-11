using System;
using BoutiqueDTO.Models.Implementations.Configuration;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueXamarinCommon.Models.Interfaces.Configuration
{
    /// <summary>
    /// Конфигурация. Трансферная модель
    /// </summary>
    public interface IXamarinConfigurationTransfer : IXamarinConfigurationBase<HostConfigurationTransfer>, ITransferModel<Guid>
    { }
}