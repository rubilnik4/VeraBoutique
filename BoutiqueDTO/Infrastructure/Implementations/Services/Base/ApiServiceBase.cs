using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.RestResponse.Async;
using BoutiqueDTO.Infrastructure.Interfaces.Refit.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Base;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueDTO.Models.Interfaces.Connection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using RestSharp;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection.ResultCollectionTryAsyncExtensions;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultValue.ResultValueTryAsyncExtensions;
using static Functional.FunctionalExtensions.Async.ResultExtension.ResultError.ResultErrorTryAsyncExtensions;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Base
{
    public abstract class ApiServiceBase<TId, TTransfer> : IApiServiceBase<TId, TTransfer>
        where TTransfer : ITransferModel<TId>
        where TId : notnull
    {
        protected ApiServiceBase(IHostConnection hostConnection)
        {
            _hostConnection = hostConnection;
        }

        /// <summary>
        /// Параметры подключения
        /// </summary>
        private readonly IHostConnection _hostConnection;

        /// <summary>
        /// Клиент для Api сервисов
        /// </summary>
        private IRestClient RestClient =>
            new RestClient(_hostConnection.Host)
            {
                Timeout = (int)TimeSpan.FromSeconds(_hostConnection.TimeOut).TotalMilliseconds,
            };

        /// <summary>
        /// Получить данные Api
        /// </summary>
        public async Task<IResultCollection<TTransfer>> Get() =>
            await RestClient.ExecuteAsync<List<TTransfer>>(ApiRestRequest.GetJsonRequest<TId, TTransfer>()).
            ToRestResultCollectionTaskAsync();

        /// <summary>
        /// Получить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TTransfer>> Get(TId id) =>
            await RestClient.ExecuteAsync<TTransfer>(ApiRestRequest.GetJsonRequest<TId, TTransfer>(id)).
            ToRestResultValueTaskAsync();

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        public async Task<IResultValue<TId>> Post(TTransfer transfer) =>
            await RestClient.ExecuteAsync<TId>(ApiRestRequest.PostJsonRequest<TId, TTransfer>(transfer)).
            ToRestResultValueTaskAsync();

        /// <summary>
        /// Добавить коллекцию данных Api
        /// </summary>
        public async Task<IResultCollection<TId>> PostCollection(IEnumerable<TTransfer> transfers) =>
            await RestClient.ExecuteAsync<List<TId>>(ApiRestRequest.PostJsonRequest<TId, TTransfer>(transfers)).
            ToRestResultCollectionTaskAsync();

        /// <summary>
        /// Обновить данные Api
        /// </summary>
        public async Task<IResultError> Put(TTransfer transfer) =>
            await RestClient.ExecuteAsync(ApiRestRequest.PutJsonRequest<TId, TTransfer>(transfer)).
            ToRestResultErrorTaskAsync();

        /// <summary>
        /// Удалить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TTransfer>> Delete(TId id) =>
            await RestClient.ExecuteAsync<TTransfer>(ApiRestRequest.DeleteJsonRequest<TId, TTransfer>(id)).
            ToRestResultValueTaskAsync();
    }
}