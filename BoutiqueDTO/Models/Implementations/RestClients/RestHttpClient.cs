using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Extensions.RestResponses.Sync;
using BoutiqueDTO.Factory.HttpClients;
using BoutiqueDTO.Models.Enums.RestClients;
using BoutiqueDTO.Models.Interfaces.RestClients;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.RestErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.RestErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Models.Implementations.RestClients
{
    /// <summary>
    /// Базовый клиент для http запросов
    /// </summary>
    public class RestHttpClient : IRestHttpClient
    {
        public RestHttpClient(HttpClientHandler httpClientHandler, Uri baseAddress, TimeSpan timeout)
        {
            HttpClientHandler = httpClientHandler;
            BaseAddress = baseAddress;
            Timeout = timeout;
        }

        /// <summary>
        /// Обработчик запросов
        /// </summary>
        protected HttpClientHandler HttpClientHandler { get; }

        /// <summary>
        /// Адрес сервера
        /// </summary>
        public Uri BaseAddress { get; }

        /// <summary>
        /// Время ожидания ответа
        /// </summary>
        public TimeSpan Timeout { get; }

        /// <summary>
        /// Клиент для http запросов
        /// </summary>
        protected virtual Task<HttpClient> GetHttpClient() =>
            Task.FromResult(HttpClientFactory.GetRestClient(HttpClientHandler, BaseAddress, Timeout));

        /// <summary>
        /// Тип авторизации
        /// </summary>
        public async Task<AuthorizationType> GetAuthorizationType() =>
            await GetHttpClient().
            MapTaskAsync(httpClient => Enum.TryParse(httpClient.DefaultRequestHeaders.Authorization?.Scheme, true,
                                                     out AuthorizationType authorizationType)
                             ? authorizationType
                             : AuthorizationType.None);

        /// <summary>
        /// Получить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TOut>> GetValueAsync<TOut>(string request)
            where TOut : notnull =>
            await GetHttpClient().
            MapBindAsync(httpClient => ResultValueTryAsyncExtensions.ResultValueTryAsync(() => httpClient.GetAsync(request), GetRestError)).
            ResultValueBindOkBindAsync(response => response.ToRestResultValueAsync<TOut>());

        /// <summary>
        /// Получить данные Api
        /// </summary>
        public async Task<IResultCollection<TOut>> GetCollectionAsync<TOut>(string request)
            where TOut : notnull =>
            await GetHttpClient().
            MapBindAsync(httpClient => ResultValueTryAsyncExtensions.ResultValueTryAsync(() => httpClient.GetAsync(request), GetRestError)).
            ResultValueBindOkToCollectionBindAsync(response => response.ToRestResultCollectionAsync<TOut>());

        /// <summary>
        /// Получить байтовый массив по идентификатору Api
        /// </summary>
        public async Task<IResultValue<byte[]>> GetByteAsync(string request) =>
            await GetHttpClient().
            MapBindAsync(httpClient => ResultValueTryAsyncExtensions.ResultValueTryAsync(() => httpClient.GetByteArrayAsync(request), GetRestError));

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        public async Task<IResultValue<string>> PostAsync(string request, string jsonContent) =>
            await GetHttpClient().
            MapBindAsync(httpClient => ResultValueTryAsyncExtensions.ResultValueTryAsync(
                () => httpClient.PostAsync(request, ToStringContent(jsonContent)), GetRestError).
            ResultValueBindOkBindAsync(response => response.ToRestResultAsync()));

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        public async Task<IResultValue<TOut>> PostValueAsync<TOut>(string request, string jsonContent)
            where TOut : notnull =>
            await GetHttpClient().
            MapBindAsync(httpClient => ResultValueTryAsyncExtensions.ResultValueTryAsync(
                () => httpClient.PostAsync(request, ToStringContent(jsonContent)), GetRestError)).
            ResultValueBindOkBindAsync(response => response.ToRestResultValueAsync<TOut>());

        /// <summary>
        /// Добавить коллекцию данных Api
        /// </summary>
        public async Task<IResultCollection<TOut>> PostCollectionAsync<TOut>(string request, string jsonContent)
            where TOut : notnull =>
            await GetHttpClient().
            MapBindAsync(httpClient => ResultValueTryAsyncExtensions.ResultValueTryAsync(
                () => httpClient.PostAsync(request, ToStringContent(jsonContent)), GetRestError)).
            ResultValueBindOkToCollectionBindAsync(response => response.ToRestResultCollectionAsync<TOut>());

        /// <summary>
        /// Обновить данные Api по идентификатору
        /// </summary>
        public async Task<IResultError> PutValueAsync(string request, string jsonContent) =>
            await GetHttpClient().
            MapBindAsync(httpClient => ResultValueTryAsyncExtensions.ResultValueTryAsync(
                () => httpClient.PutAsync(request, ToStringContent(jsonContent)), GetRestError)).
            ResultValueBindErrorsOkBindAsync(response => response.ToRestResultError());

        /// <summary>
        /// Удалить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TOut>> DeleteValueAsync<TOut>(string request)
            where TOut : notnull =>
            await GetHttpClient().
            MapBindAsync(httpClient => ResultValueTryAsyncExtensions.ResultValueTryAsync(() => httpClient.DeleteAsync(request), GetRestError)).
            ResultValueBindOkBindAsync(response => response.ToRestResultValueAsync<TOut>());

        /// <summary>
        /// Удалить данные Api
        /// </summary>
        public async Task<IResultError> DeleteCollectionAsync(string request) =>
             await GetHttpClient().
            MapBindAsync(httpClient => ResultValueTryAsyncExtensions.ResultValueTryAsync(() => httpClient.DeleteAsync(request), GetRestError)).
            ResultValueBindErrorsOkBindAsync(response => response.ToRestResultError());

        /// <summary>
        /// Получить ошибку сервера по исключению
        /// </summary>
        private IRestErrorResult GetRestError(Exception exception) =>
            exception switch
            {
                TaskCanceledException _ => ErrorResultFactory.RestTimeoutError(BaseAddress.Host, Timeout, "Вышло время ожидания клиента"),
                _ => UnknownRestError,
            };

        /// <summary>
        /// Ошибка. Сервер не найден
        /// </summary>
        private IRestErrorResult UnknownRestError =>
             ErrorResultFactory.RestHostError(RestErrorType.UnknownRestStatus, BaseAddress.Host, $"Сервер {BaseAddress.Host} не найден");


        /// <summary>
        /// Преобразование в json
        /// </summary>
        private static StringContent ToStringContent(string jsonContent) =>
            new StringContent(jsonContent, Encoding.UTF8, "application/json");
    }
}