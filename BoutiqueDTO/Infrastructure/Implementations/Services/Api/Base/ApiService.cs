using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.ReflectionExtensions;
using BoutiqueCommon.Extensions.StringExtensions;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Base;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueDTO.Models.Interfaces.Connection;
using Functional.Models.Interfaces.Result;
using RestSharp;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base
{
    /// <summary>
    /// Базовый сервис работы с api
    /// </summary>
    public abstract class ApiService<TId, TTransfer> : IApiService<TId, TTransfer>
        where TTransfer : ITransferModel<TId>
        where TId : notnull
    {
        protected ApiService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        /// <summary>
        /// Клиент для Api сервисов
        /// </summary>
        private readonly IRestClient _restClient;

        /// <summary>
        /// Наименование контроллера
        /// </summary>
        private string ControllerName => 
            GetType().Name.SubstringRemove(typeof(ApiService<TId, TTransfer>).GetNameWithoutGenerics());

        /// <summary>
        /// Получить данные Api
        /// </summary>
        public async Task<IResultCollection<TTransfer>> Get() =>
            await _restClient.ExecuteAsync<List<TTransfer>>(ApiRestRequest.GetJsonRequest(ControllerName)).
            ToRestResultCollectionAsync();

        /// <summary>
        /// Получить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TTransfer>> Get(TId id) =>
            await _restClient.ExecuteAsync<TTransfer>(ApiRestRequest.GetJsonRequest(id, ControllerName)).
            ToRestResultValueAsync();

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        public async Task<IResultValue<TId>> Post(TTransfer transfer) =>
            await _restClient.ExecuteAsync<TId>(ApiRestRequest.PostJsonRequest<TId, TTransfer>(transfer, ControllerName)).
            ToRestResultValueAsync();

        /// <summary>
        /// Добавить коллекцию данных Api
        /// </summary>
        public async Task<IResultCollection<TId>> PostCollection(IEnumerable<TTransfer> transfers) =>
            await _restClient.ExecuteAsync<List<TId>>(ApiRestRequest.PostJsonRequest<TId, TTransfer>(transfers, ControllerName)).
            ToRestResultCollectionAsync();

        /// <summary>
        /// Обновить данные Api
        /// </summary>
        public async Task<IResultError> Put(TTransfer transfer) =>
            await _restClient.ExecuteAsync(ApiRestRequest.PutJsonRequest<TId, TTransfer>(transfer, ControllerName)).
            ToRestResultErrorAsync();

        /// <summary>
        /// Удалить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TTransfer>> Delete(TId id) =>
            await _restClient.ExecuteAsync<TTransfer>(ApiRestRequest.DeleteJsonRequest(id, ControllerName)).
            ToRestResultValueAsync();
    }
}