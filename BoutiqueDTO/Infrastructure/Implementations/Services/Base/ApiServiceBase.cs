using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Interfaces.Refit.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Base;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueDTO.Models.Interfaces.Connection;
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
        public async Task<IResultCollection<TTransfer>> GetApi() =>
            await RestClient.ExecuteAsync<List<TTransfer>>(ApiRestRequest.GetJsonRequest<TId, TTransfer>());

        /// <summary>
        /// Получить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TTransfer>> GetApi(TId id) =>
            await RestClient.ExecuteAsync<TTransfer>(ApiRestRequest.GetJsonRequest<TId, TTransfer>(id));

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        public async Task<IResultValue<TId>> PostApi(TTransfer transfer) =>
            await RestClient.ExecuteAsync<TId>(ApiRestRequest.PostJsonRequest<TId, TTransfer>(transfer));

        /// <summary>
        /// Добавить коллекцию данных Api
        /// </summary>
        public async Task<IResultCollection<TId>> PostCollectionApi(IEnumerable<TTransfer> transfers) =>
            await RestClient.ExecuteAsync<IReadOnlyCollection<TId>>(ApiRestRequest.PostJsonRequest<TId, TTransfer>(transfers));

        /// <summary>
        /// Обновить данные Api
        /// </summary>
        public async Task<IResultError> PutApi(TTransfer transfer) =>
            await RestClient.ExecuteAsync(ApiRestRequest.PutJsonRequest<TId, TTransfer>(transfer));

        /// <summary>
        /// Удалить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TTransfer>> DeleteApi(TId id) =>
            await RestClient.ExecuteAsync<TTransfer>(ApiRestRequest.DeleteJsonRequest<TId, TTransfer>(id));

        private static IErrorResult TransferError => new ErrorResult(ErrorResultType.Unknown, String.Empty);
    }
}