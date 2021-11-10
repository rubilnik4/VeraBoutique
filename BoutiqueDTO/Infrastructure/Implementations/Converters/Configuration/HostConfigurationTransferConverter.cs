using System;
using BoutiqueCommon.Models.Common.Interfaces.Configuration;
using BoutiqueCommon.Models.Domain.Implementations.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Configuration;
using BoutiqueDTO.Models.Implementations.Configuration;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

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
            GetHostConfigurationFunc().
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
        private static IResultValue<Func<Uri, TimeSpan, IHostConfigurationDomain>> GetHostConfigurationFunc() =>
            new ResultValue<Func<Uri, TimeSpan, IHostConfigurationDomain>>(
                (host, timeOut) => new HostConfigurationDomain(host, timeOut));
    }
}