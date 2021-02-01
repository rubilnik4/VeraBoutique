using BoutiqueCommon.Models.Domain.Implementations.Configuration;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Configuration;
using BoutiqueDTO.Models.Implementations.Configuration;
using BoutiqueDTO.Models.Implementations.Identity;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Configuration
{
    /// <summary>
    /// Преобразование параметров сервера в трансферную модель
    /// </summary>
    public class HostConfigurationTransferConverter : TransferConverter<string, IHostConfigurationDomain, HostConfigurationTransfer>,
                                                      IHostConfigurationTransferConverter
    {
        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override HostConfigurationTransfer ToTransfer(IHostConfigurationDomain hostConfigurationDomain) =>
            new HostConfigurationTransfer(hostConfigurationDomain);

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IResultValue<IHostConfigurationDomain> FromTransfer(HostConfigurationTransfer hostConfigurationTransfer) =>
            new HostConfigurationDomain(hostConfigurationTransfer).
            Map(hostConfigurationDomain => new ResultValue<IHostConfigurationDomain>(hostConfigurationDomain));
    }
}