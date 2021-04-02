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
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
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
                                  ITransferConverter<TId, TDomain, TTransfer> transferConverter)
        {
            _apiService = apiService;
            _transferConverter = transferConverter;
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
        /// Получить данные
        /// </summary>
        public IResultCollection<TDomain> Get() =>
            new ResultValue<IApiService<TId, TTransfer>>(_apiService).
            ResultValueBindOkToCollection(api => api.Get()).
            ResultCollectionBindOk(transfers => _transferConverter.FromTransfers(transfers));

        /// <summary>
        /// Получить данные по идентификатору
        /// </summary>
        public IResultValue<TDomain> Get(TId id) =>
            new ResultValue<IApiService<TId, TTransfer>>(_apiService).
            ResultValueBindOk(api => api.Get(id)).
            ResultValueBindOk(transfer => _transferConverter.FromTransfer(transfer));

        /// <summary>
        /// Отправить данные
        /// </summary>
        public IResultError Post(IEnumerable<TDomain> domains) =>
            new ResultValue<IApiService<TId, TTransfer>>(_apiService).
            ResultValueBindOkToCollection(api => api.PostCollection(_transferConverter.ToTransfers(domains)));

        /// <summary>
        /// Удалить все данные
        /// </summary>
        public IResultError Delete() =>
            new ResultValue<IApiService<TId, TTransfer>>(_apiService).
            ResultValueBindErrorsOk(api => api.Delete());

        /// <summary>
        /// Получить данные асинхронно
        /// </summary>
        public async Task<IResultCollection<TDomain>> GetAsync() =>
            await new ResultValue<IApiService<TId, TTransfer>>(_apiService).
            ResultValueBindOkToCollectionAsync(api => api.GetAsync()).
            ResultCollectionBindOkTaskAsync(transfers => _transferConverter.FromTransfers(transfers));

        /// <summary>
        /// Получить данные по идентификатору асинхронно
        /// </summary>
        public async Task<IResultValue<TDomain>> GetAsync(TId id) =>
            await new ResultValue<IApiService<TId, TTransfer>>(_apiService).
            ResultValueBindOkAsync(api => api.GetAsync(id)).
            ResultValueBindOkTaskAsync(transfer => _transferConverter.FromTransfer(transfer));

        /// <summary>
        /// Отправить данные асинхронно
        /// </summary>
        public async Task<IResultError> PostAsync(IEnumerable<TDomain> domains) =>
            await new ResultValue<IApiService<TId, TTransfer>>(_apiService).
            ResultValueBindOkToCollectionAsync(api => api.PostCollectionAsync(_transferConverter.ToTransfers(domains)));

        /// <summary>
        /// Удалить все данные асинхронно
        /// </summary>
        public async Task<IResultError> DeleteAsync() =>
            await new ResultValue<IApiService<TId, TTransfer>>(_apiService).
            ResultValueBindErrorsOkAsync(api => api.DeleteAsync());
    }
}