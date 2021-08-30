using System;
using BoutiqueCommon.Models.Common.Interfaces.Configuration;
using BoutiqueCommon.Models.Domain.Implementations.Configuration;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Configuration;
using BoutiqueDTO.Models.Implementations.Configuration;
using BoutiqueDTO.Models.Implementations.Identity;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;

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
            GetHostConfigurationFunc(hostConfigurationTransfer).
            ResultValueCurryOk(hostConfigurationTransfer.Host.
                               ToResultValueNullCheck(ErrorResultFactory.ValueNotFoundError(hostConfigurationTransfer.Host, GetType()))).
            ResultValueCurryOk(hostConfigurationTransfer.TimeOut.
                               ToResultValueWhere(timeOut => timeOut.TotalSeconds > 0,
                                    timeOut => ErrorResultFactory.ValueNotValidError(timeOut.TotalSeconds, GetType(),
                                                                                     $"Значение {nameof(hostConfigurationTransfer.TimeOut)} должно быть больше 0"))).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения конфигурации
        /// </summary>
        private static IResultValue<Func<Uri, TimeSpan, IHostConfigurationDomain>> GetHostConfigurationFunc(IHostConfigurationBase hostConfiguration) =>
            new ResultValue<Func<Uri, TimeSpan, IHostConfigurationDomain>>(
                (host, timeOut) => new HostConfigurationDomain(host, timeOut, hostConfiguration.DisableSSL));
    }
}