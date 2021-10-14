using System;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueConsole.Infrastructure.Interfaces.Converters;
using BoutiqueConsole.Models.Implementations.Configuration;
using BoutiqueConsole.Models.Interfaces.Configuration;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Configuration;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueConsole.Infrastructure.Implementations.Converters
{
    /// <summary>
    /// Конвертер конфигурации консольного загрузчика в трансферную модель
    /// </summary>
    public class LoaderConfigurationTransferConverter : TransferConverter<Guid, ILoaderConfigurationDomain, LoaderConfigurationTransfer>,
                                                        ILoaderConfigurationTransferConverter
    {
        public LoaderConfigurationTransferConverter(IHostConfigurationTransferConverter hostConfigurationTransferConverter)
        {
            _hostConfigurationTransferConverter = hostConfigurationTransferConverter;
        }

        /// <summary>
        /// Конвертер размеров одежды в трансферную модель
        /// </summary>
        private readonly IHostConfigurationTransferConverter _hostConfigurationTransferConverter;

        /// <summary>
        /// Преобразовать конфигурацию в трансферную модель
        /// </summary>
        public override LoaderConfigurationTransfer ToTransfer(ILoaderConfigurationDomain xamarinConfigurationDomain) =>
            new (_hostConfigurationTransferConverter.ToTransfer(xamarinConfigurationDomain.HostConfiguration));

        /// <summary>
        /// Преобразовать конфигурацию из трансферной модели
        /// </summary>
        public override IResultValue<ILoaderConfigurationDomain> FromTransfer(LoaderConfigurationTransfer xamarinConfigurationTransfer) =>
            GetXamarinConfigurationFunc().
            ResultValueCurryOk(_hostConfigurationTransferConverter.GetDomain(xamarinConfigurationTransfer.HostConfiguration)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения конфигурации
        /// </summary>
        private static IResultValue<Func<IHostConfigurationDomain, ILoaderConfigurationDomain>> GetXamarinConfigurationFunc() =>
            new ResultValue<Func<IHostConfigurationDomain, ILoaderConfigurationDomain>>(
                hostConfiguration => new LoaderConfigurationDomain(hostConfiguration));
    }
}