using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base
{
    /// <summary>
    /// Базовый сервис для данных Api
    /// </summary>
    public abstract class RestServiceBase<TId, TDomain, TTransfer> : IRestServiceBase<TId, TDomain>
        where TDomain : IDomainModel<TId>
        where TTransfer : class, ITransferModel<TId>
        where TId : notnull
    {
        protected RestServiceBase(IApiService<TId, TTransfer> apiService,
                                  ITransferConverter<TId, TDomain, TTransfer> transferConverter,
                                  IBoutiqueLogger boutiqueLogger)
        {
            _apiService = apiService;
            _transferConverter = transferConverter;
            _boutiqueLogger = boutiqueLogger;
        }

        /// <summary>
        /// Сервис получения данных по протоколу rest api
        /// </summary>
        private readonly IApiService<TId, TTransfer> _apiService;

        /// <summary>
        /// Конвертер из доменной модели в трансферную модель
        /// </summary>
        private readonly ITransferConverter<TId, TDomain, TTransfer> _transferConverter;

        /// <summary>
        /// Логгер
        /// </summary>
        private readonly IBoutiqueLogger _boutiqueLogger;

        /// <summary>
        /// Отправить данные
        /// </summary>
        public async Task<IResultCollection<TDomain>> Get() =>
            await new ResultValue<IApiService<TId, TTransfer>>(_apiService).
            ResultValueBindOkToCollectionAsync(api => api.Get()).
            ResultCollectionBindOkTaskAsync(transfers => _transferConverter.FromTransfers(transfers)).
            VoidTaskAsync(result => ServiceLog(result, nameof(Get)));

        /// <summary>
        /// Отправить данные
        /// </summary>
        public async Task<IResultError> Post(IEnumerable<TDomain> domains) =>
            await new ResultValue<IApiService<TId, TTransfer>>(_apiService).
            ResultValueBindOkToCollectionAsync(api => api.PostCollection(_transferConverter.ToTransfers(domains))).
            VoidTaskAsync(result => ServiceLog(result, nameof(Post)));

        /// <summary>
        /// Удалить все данные
        /// </summary>
        public async Task<IResultError> Delete() =>
            await new ResultValue<IApiService<TId, TTransfer>>(_apiService).
            ResultValueBindErrorsOkAsync(api => api.Delete()).
            VoidTaskAsync(result => ServiceLog(result, nameof(Delete)));

        /// <summary>
        /// Логгирование
        /// </summary>
        private void ServiceLog(IResultError result, string actionType) =>
           result.
           ResultErrorVoidOkBad(() => _boutiqueLogger.ShowMessage($"{actionType} [{typeof(TDomain).Name}] completed"),
                                errors => errors.
                                          Void(_ => _boutiqueLogger.ShowMessage($"Error {actionType} [{typeof(TDomain).Name}]")).
                                          Void(_ => _boutiqueLogger.ShowErrors(errors)));
    }
}