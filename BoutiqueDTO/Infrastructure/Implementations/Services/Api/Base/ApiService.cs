using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.ReflectionExtensions;
using BoutiqueCommon.Extensions.StringExtensions;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Extensions.RestResponses.Sync;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base
{
    /// <summary>
    /// Базовый сервис получения данных по протоколу rest api
    /// </summary>
    public abstract class ApiService<TId, TTransfer> : ApiServiceBase<TId, TTransfer>, IApiService<TId, TTransfer>
        where TTransfer : ITransferModel<TId>
        where TId : notnull
    {
        protected ApiService(HttpClient httpClient)
            : base(httpClient)
        { }

        /// <summary>
        /// Получить данные Api
        /// </summary>
        public async Task<IResultCollection<TTransfer>> GetAsync() =>
             await HttpClient.GetAsync(ApiRestRequest.GetRequest(ControllerName)).
             ToRestResultCollectionTaskAsync<TTransfer>();

        /// <summary>
        /// Получить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TTransfer>> GetAsync(TId id) =>
            await HttpClient.GetAsync(ApiRestRequest.GetRequest(id, ControllerName)).
            ToRestResultValueTaskAsync<TTransfer>();

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        public async Task<IResultValue<TId>> PostAsync(TTransfer transfer) =>
            await transfer.ToJsonTransfer().
            ResultValueOkAsync(json => HttpClient.PostAsync(ApiRestRequest.PostRequest(ControllerName),
                                                            new StringContent(json))).
            ResultValueBindOkBindAsync(response => response.ToRestResultValueAsync<TId>());

        /// <summary>
        /// Добавить коллекцию данных Api
        /// </summary>
        public async Task<IResultCollection<TId>> PostCollectionAsync(IEnumerable<TTransfer> transfers) =>
            await transfers.ToJsonTransfer().
            ResultValueOkAsync(json => HttpClient.PostAsync(ApiRestRequest.PostRequestCollection(ControllerName),
                                                            new StringContent(json))).
            ResultValueBindOkToCollectionBindAsync(response => response.ToRestResultCollectionAsync<TId>());

        /// <summary>
        /// Обновить данные Api
        /// </summary>
        public async Task<IResultError> PutAsync(TTransfer transfer) =>
            await transfer.ToJsonTransfer().
            ResultValueOkAsync(json => HttpClient.PutAsync(ApiRestRequest.PostRequest(ControllerName),
                                                            new StringContent(json))).
            ResultValueBindErrorsOkTaskAsync(response => response.ToRestResultError());

        /// <summary>
        /// Удалить все данные Api
        /// </summary>
        public async Task<IResultError> DeleteAsync() =>
            await HttpClient.DeleteAsync(ApiRestRequest.GetRequest(ControllerName)).
            ToRestResultErrorTaskAsync();

        /// <summary>
        /// Удалить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TTransfer>> DeleteAsync(TId id) =>
            await HttpClient.DeleteAsync(ApiRestRequest.GetRequest(id, ControllerName)).
            ToRestResultValueTaskAsync<TTransfer>();
    }
}