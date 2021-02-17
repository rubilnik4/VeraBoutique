using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.ReflectionExtensions;
using BoutiqueCommon.Extensions.StringExtensions;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Extensions.RestResponses.Sync;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base;
using BoutiqueDTO.Models.Interfaces.Base;
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
        protected ApiService(IRestClient restClient)
            : base(restClient)
        { }

        /// <summary>
        /// Получить данные Api
        /// </summary>
        public IResultCollection<TTransfer> Get() =>
            RestClient.Execute<List<TTransfer>>(ApiRestRequest.GetJsonRequest(ControllerName)).
            ToRestResultCollection();

        /// <summary>
        /// Получить данные по идентификатору Api
        /// </summary>
        public IResultValue<TTransfer> Get(TId id) =>
            RestClient.Execute<TTransfer>(ApiRestRequest.GetJsonRequest(id, ControllerName)).
            ToRestResultValue();

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        public IResultValue<TId> Post(TTransfer transfer) =>
            RestClient.Execute<TId>(ApiRestRequest.PostJsonRequest<TId, TTransfer>(transfer, ControllerName)).
            ToRestResultValue();

        /// <summary>
        /// Добавить коллекцию данных Api
        /// </summary>
        public IResultCollection<TId> PostCollection(IEnumerable<TTransfer> transfers) =>
            RestClient.Execute<List<TId>>(ApiRestRequest.PostJsonRequest<TId, TTransfer>(transfers, ControllerName)).
            ToRestResultCollection();

        /// <summary>
        /// Обновить данные Api
        /// </summary>
        public IResultError Put(TTransfer transfer) =>
            RestClient.Execute(ApiRestRequest.PutJsonRequest<TId, TTransfer>(transfer, ControllerName)).
            ToRestResultError();

        /// <summary>
        /// Удалить все данные Api
        /// </summary>
        public IResultError Delete() =>
            RestClient.Execute(ApiRestRequest.DeleteJsonRequest(ControllerName)).
            ToRestResultError();

        /// <summary>
        /// Удалить данные по идентификатору Api
        /// </summary>
        public IResultValue<TTransfer> Delete(TId id) =>
            RestClient.Execute<TTransfer>(ApiRestRequest.DeleteJsonRequest(id, ControllerName)).
            ToRestResultValue();

        /// <summary>
        /// Получить данные Api
        /// </summary>
        public async Task<IResultCollection<TTransfer>> GetAsync() =>
            await RestClient.ExecuteAsync<List<TTransfer>>(ApiRestRequest.GetJsonRequest(ControllerName)).
            ToRestResultCollectionAsync();

        /// <summary>
        /// Получить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TTransfer>> GetAsync(TId id) =>
            await RestClient.ExecuteAsync<TTransfer>(ApiRestRequest.GetJsonRequest(id, ControllerName)).
            ToRestResultValueAsync();

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        public async Task<IResultValue<TId>> PostAsync(TTransfer transfer) =>
            await RestClient.ExecuteAsync<TId>(ApiRestRequest.PostJsonRequest<TId, TTransfer>(transfer, ControllerName)).
            ToRestResultValueAsync();

        /// <summary>
        /// Добавить коллекцию данных Api
        /// </summary>
        public async Task<IResultCollection<TId>> PostCollectionAsync(IEnumerable<TTransfer> transfers) =>
            await RestClient.ExecuteAsync<List<TId>>(ApiRestRequest.PostJsonRequest<TId, TTransfer>(transfers, ControllerName)).
            ToRestResultCollectionAsync();

        /// <summary>
        /// Обновить данные Api
        /// </summary>
        public async Task<IResultError> PutAsync(TTransfer transfer) =>
            await RestClient.ExecuteAsync(ApiRestRequest.PutJsonRequest<TId, TTransfer>(transfer, ControllerName)).
            ToRestResultErrorAsync();

        /// <summary>
        /// Удалить все данные Api
        /// </summary>
        public async Task<IResultError> DeleteAsync() =>
            await RestClient.ExecuteAsync(ApiRestRequest.DeleteJsonRequest(ControllerName)).
            ToRestResultErrorAsync();

        /// <summary>
        /// Удалить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TTransfer>> DeleteAsync(TId id) =>
            await RestClient.ExecuteAsync<TTransfer>(ApiRestRequest.DeleteJsonRequest(id, ControllerName)).
            ToRestResultValueAsync();
    }
}