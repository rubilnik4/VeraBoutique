using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Configuration;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueXamarin.Infrastructure.Interfaces.Converters;
using BoutiqueXamarin.Models.Implementation.Configuration;
using BoutiqueXamarin.Models.Interfaces.Configuration;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueXamarin.Infrastructure.Implementations.Converters
{
    /// <summary>
    /// Конвертер конфигурации Xamarin  в трансферную модель
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
            ResultCurryBindOk(_hostConfigurationTransferConverter.GetDomain(xamarinConfigurationTransfer.HostConfiguration)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения конфигурации
        /// </summary>
        private static IResultValue<Func<IHostConfigurationDomain, IXamarinConfigurationDomain>> GetXamarinConfigurationFunc() =>
            new ResultValue<Func<IHostConfigurationDomain, IXamarinConfigurationDomain>>(
                hostConfiguration => new XamarinConfigurationDomain(hostConfiguration));
    }
}