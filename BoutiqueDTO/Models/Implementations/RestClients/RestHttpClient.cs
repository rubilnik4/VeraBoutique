using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Extensions.RestResponses.Sync;
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
    /// Клиент для http запросов
    /// </summary>
    public class RestHttpClient : IRestHttpClient
    {
        public RestHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Клиент для http запросов
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Адрес сервера
        /// </summary>
        public Uri BaseAddress =>
            _httpClient.BaseAddress;

        /// <summary>
        /// Время ожидания ответа
        /// </summary>
        public TimeSpan TimeOut =>
            _httpClient.Timeout;

        /// <summary>
        /// Тип авторизации
        /// </summary>
        public AuthorizationType AuthorizationType =>
            Enum.TryParse(_httpClient.DefaultRequestHeaders.Authorization?.Scheme, true, out AuthorizationType authorizationType)
                ? authorizationType
                : AuthorizationType.None;

        /// <summary>
        /// Токен авторизации
        /// </summary>
        public string? JwtToken =>
             _httpClient.DefaultRequestHeaders.Authorization?.Parameter;

        /// <summary>
        /// Получить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TOut>> GetValueAsync<TOut>(string request)
            where TOut : notnull =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(() => _httpClient.GetAsync(request), GetRestError).
            ResultValueBindOkBindAsync(response => response.ToRestResultValueAsync<TOut>());

        /// <summary>
        /// Получить данные Api
        /// </summary>
        public async Task<IResultCollection<TOut>> GetCollectionAsync<TOut>(string request)
            where TOut : notnull =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(() => _httpClient.GetAsync(request), GetRestError).
            ResultValueBindOkToCollectionBindAsync(response => response.ToRestResultCollectionAsync<TOut>());

        /// <summary>
        /// Получить байтовый массив по идентификатору Api
        /// </summary>
        public async Task<IResultValue<byte[]>> GetByteAsync(string request) =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(() => _httpClient.GetByteArrayAsync(request), GetRestError);

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        public async Task<IResultValue<string>> PostAsync(string request, string jsonContent) =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(
                () => _httpClient.PostAsync(request, ToStringContent(jsonContent)), GetRestError).
            ResultValueBindOkBindAsync(response => response.ToRestResultAsync());

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        public async Task<IResultValue<TOut>> PostValueAsync<TOut>(string request, string jsonContent)
            where TOut : notnull =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(
                () => _httpClient.PostAsync(request, ToStringContent(jsonContent)), GetRestError).
            ResultValueBindOkBindAsync(response => response.ToRestResultValueAsync<TOut>());

        /// <summary>
        /// Добавить коллекцию данных Api
        /// </summary>
        public async Task<IResultCollection<TOut>> PostCollectionAsync<TOut>(string request, string jsonContent)
            where TOut : notnull =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(
                () => _httpClient.PostAsync(request, ToStringContent(jsonContent)), GetRestError).
            ResultValueBindOkToCollectionBindAsync(response => response.ToRestResultCollectionAsync<TOut>());

        /// <summary>
        /// Обновить данные Api по идентификатору
        /// </summary>
        public async Task<IResultError> PutValueAsync(string request, string jsonContent) =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(
                () => _httpClient.PutAsync(request, ToStringContent(jsonContent)), GetRestError).
            ResultValueBindErrorsOkTaskAsync(response => response.ToRestResultError());

        /// <summary>
        /// Удалить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TOut>> DeleteValueAsync<TOut>(string request)
            where TOut : notnull =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(() => _httpClient.DeleteAsync(request), GetRestError).
            ResultValueBindOkBindAsync(response => response.ToRestResultValueAsync<TOut>());

        /// <summary>
        /// Удалить данные Api
        /// </summary>
        public async Task<IResultError> DeleteCollectionAsync(string request) =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(() => _httpClient.DeleteAsync(request), GetRestError).
            ResultValueBindErrorsOkTaskAsync(response => response.ToRestResultError());

        /// <summary>
        /// Получить ошибку сервера по исключению
        /// </summary>
        private IRestErrorResult GetRestError(Exception exception) =>
            exception switch
            {
                TaskCanceledException _ => ErrorResultFactory.RestTimeoutError(BaseAddress.Host, TimeOut, "Вышло время ожидания клиента"),
                _ => UnknownRestError,
            };

        /// <summary>
        /// Ошибка. Сервер не найден
        /// </summary>
        private IRestErrorResult UnknownRestError =>
             ErrorResultFactory.RestHostError(RestErrorType.UnknownRestStatus, BaseAddress.Host,
                                              $"Сервер {BaseAddress.Host} не найден");


        /// <summary>
        /// Преобразование в json
        /// </summary>
        private static StringContent ToStringContent(string jsonContent) =>
            new StringContent(jsonContent, Encoding.UTF8, "application/json");
    }
}