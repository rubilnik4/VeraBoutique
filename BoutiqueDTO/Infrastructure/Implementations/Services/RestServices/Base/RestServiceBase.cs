using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.ReflectionExtensions;
using BoutiqueCommon.Extensions.StringExtensions;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base
{
    /// <summary>
    /// Базовый сервис для данных Api
    /// </summary>
    public abstract class RestServiceBase<TId, TDomain, TTransfer> : RestService<TId, TDomain, TTransfer>,
                                                                     IRestServiceBase<TId, TDomain>
        where TDomain : IDomainModel<TId>
        where TTransfer : class, ITransferModel<TId>
        where TId : notnull
    {
        protected RestServiceBase(IRestHttpClient restHttpClient,
                                  ITransferConverter<TId, TDomain, TTransfer> transferConverter)
        {
            RestHttpClient = restHttpClient;
            _transferConverter = transferConverter;
        }

        /// <summary>
        /// Клиент для http запросов
        /// </summary>
        protected IRestHttpClient RestHttpClient;

        /// <summary>
        /// Конвертер из доменной модели в трансферную модель
        /// </summary>
        private readonly ITransferConverter<TId, TDomain, TTransfer> _transferConverter;

        /// <summary>
        /// Получить данные асинхронно
        /// </summary>
        public async Task<IResultCollection<TDomain>> GetAsync() =>
            await RestHttpClient.GetCollectionAsync<TTransfer>(RestRequest.GetRequest(ControllerName)).
            ResultCollectionBindOkTaskAsync(transfers => _transferConverter.FromTransfers(transfers));

        /// <summary>
        /// Получить данные по идентификатору асинхронно
        /// </summary>
        public async Task<IResultValue<TDomain>> GetAsync(TId id) =>
            await RestHttpClient.GetValueAsync<TTransfer>(RestRequest.GetRequest(id, ControllerName)).
            ResultValueBindOkTaskAsync(transfer => _transferConverter.FromTransfer(transfer));

        /// <summary>
        /// Отправить данные асинхронно
        /// </summary>
        public async Task<IResultValue<string>> PostAsync(TDomain domain) =>
            await _transferConverter.ToTransfer(domain).
            ToJsonTransfer().
            ResultValueBindOkAsync(json =>
                RestHttpClient.PostAsync(RestRequest.PostRequest(ControllerName), json));

        /// <summary>
        /// Отправить данные асинхронно
        /// </summary>
        public async Task<IResultValue<TId>> PostValueAsync(TDomain domain) =>
            await _transferConverter.ToTransfer(domain).
            ToJsonTransfer().
            ResultValueBindOkAsync(json =>
                RestHttpClient.PostValueAsync<TId>(RestRequest.PostRequest(ControllerName), json));


        /// <summary>
        /// Отправить данные коллекции асинхронно
        /// </summary>
        public async Task<IResultCollection<TId>> PostCollectionAsync(IEnumerable<TDomain> domains) =>
            await _transferConverter.ToTransfers(domains).
            ToJsonTransfer().
            ResultValueBindOkToCollectionAsync(json => 
                RestHttpClient.PostCollectionAsync<TId>(RestRequest.PostRequestCollection(ControllerName), json));

        /// <summary>
        /// Обновить данные асинхронно
        /// </summary>
        public async Task<IResultError> PutAsync(TDomain domain) =>
            await _transferConverter.ToTransfer(domain).
            ToJsonTransfer().
            ResultValueBindErrorsOkAsync(json =>
                RestHttpClient.PutValueAsync(RestRequest.PostRequestCollection(ControllerName), json));

        /// <summary>
        /// Удалить все данные асинхронно
        /// </summary>
        public async Task<IResultError> DeleteAsync() =>
            await RestHttpClient.DeleteCollectionAsync(RestRequest.GetRequest(ControllerName));

        /// <summary>
        /// Удалить данные по идентификатору асинхронно
        /// </summary>
        public async Task<IResultValue<TDomain>> DeleteAsync(TId id) =>
            await RestHttpClient.DeleteValueAsync<TTransfer>(RestRequest.GetRequest(id, ControllerName)).
            ResultValueBindOkTaskAsync(transfer => _transferConverter.FromTransfer(transfer));
    }
}