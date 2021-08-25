using System;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Configuration;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Converters;
using BoutiqueXamarinCommon.Models.Implementation.Configuration;
using BoutiqueXamarinCommon.Models.Interfaces.Configuration;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;

namespace BoutiqueXamarinCommon.Infrastructure.Implementations.Converters
{
    /// <summary>
    /// Конвертер конфигурации Xamarin в трансферную модель
    /// </summary>
    public class XamarinConfigurationTransferConverter : TransferConverter<Guid, IXamarinConfigurationDomain, XamarinConfigurationTransfer>,
                                                         IXamarinConfigurationTransferConverter
    {
        public XamarinConfigurationTransferConverter(IHostConfigurationTransferConverter hostConfigurationTransferConverter)
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
        public override XamarinConfigurationTransfer ToTransfer(IXamarinConfigurationDomain xamarinConfigurationDomain) =>
            new XamarinConfigurationTransfer(_hostConfigurationTransferConverter.ToTransfer(xamarinConfigurationDomain.HostConfiguration));

        /// <summary>
        /// Преобразовать конфигурацию из трансферной модели
        /// </summary>
        public override IResultValue<IXamarinConfigurationDomain> FromTransfer(XamarinConfigurationTransfer xamarinConfigurationTransfer) =>
            GetXamarinConfigurationFunc().
            ResultValueCurryOk(_hostConfigurationTransferConverter.GetDomain(xamarinConfigurationTransfer.HostConfiguration)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения конфигурации
        /// </summary>
        private static IResultValue<Func<IHostConfigurationDomain, IXamarinConfigurationDomain>> GetXamarinConfigurationFunc() =>
            new ResultValue<Func<IHostConfigurationDomain, IXamarinConfigurationDomain>>(
                hostConfiguration => new XamarinConfigurationDomain(hostConfiguration));
    }
}