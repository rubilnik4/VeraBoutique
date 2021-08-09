using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Extensions.RestResponses.Sync;
using BoutiqueDTO.Models.Enums.RestClients;
using BoutiqueDTO.Models.Interfaces.RestClients;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

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
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(() => _httpClient.GetAsync(request), ServerNotFoundError).
            ResultValueBindOkBindAsync(response => response.ToRestResultValueAsync<TOut>());

        /// <summary>
        /// Получить данные Api
        /// </summary>
        public async Task<IResultCollection<TOut>> GetCollectionAsync<TOut>(string request)
            where TOut : notnull =>
             await ResultValueTryAsyncExtensions.ResultValueTryAsync(() => _httpClient.GetAsync(request), ServerNotFoundError).
             ResultValueBindOkToCollectionBindAsync(response => response.ToRestResultCollectionAsync<TOut>());

        /// <summary>
        /// Получить байтовый массив по идентификатору Api
        /// </summary>
        public async Task<IResultValue<byte[]>> GetByteAsync(string request) =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(() => _httpClient.GetByteArrayAsync(request), ServerNotFoundError);

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        public async Task<IResultValue<string>> PostAsync(string request, string jsonContent) =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(
                () => _httpClient.PostAsync(request, ToStringContent(jsonContent)), ServerNotFoundError).
            ResultValueBindOkBindAsync(response => response.ToRestResultAsync());

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        public async Task<IResultValue<TOut>> PostValueAsync<TOut>(string request, string jsonContent)
            where TOut : notnull =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(
                () => _httpClient.PostAsync(request, ToStringContent(jsonContent)), ServerNotFoundError).
            ResultValueBindOkBindAsync(response => response.ToRestResultValueAsync<TOut>());

        /// <summary>
        /// Добавить коллекцию данных Api
        /// </summary>
        public async Task<IResultCollection<TOut>> PostCollectionAsync<TOut>(string request, string jsonContent)
            where TOut : notnull =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(
                () => _httpClient.PostAsync(request, ToStringContent(jsonContent)), ServerNotFoundError).
            ResultValueBindOkToCollectionBindAsync(response => response.ToRestResultCollectionAsync<TOut>());

        /// <summary>
        /// Обновить данные Api по идентификатору
        /// </summary>
        public async Task<IResultError> PutValueAsync(string request, string jsonContent) =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(
                () => _httpClient.PutAsync(request, ToStringContent(jsonContent)), ServerNotFoundError).
            ResultValueBindErrorsOkTaskAsync(response => response.ToRestResultError());

        /// <summary>
        /// Удалить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TOut>> DeleteValueAsync<TOut>(string request)
            where TOut : notnull =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(() => _httpClient.DeleteAsync(request), ServerNotFoundError).
            ResultValueBindOkBindAsync(response => response.ToRestResultValueAsync<TOut>());

        /// <summary>
        /// Удалить данные Api
        /// </summary>
        public async Task<IResultError> DeleteCollectionAsync(string request) =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(() => _httpClient.DeleteAsync(request), ServerNotFoundError).
            ResultValueBindErrorsOkTaskAsync(response => response.ToRestResultError());

        /// <summary>
        /// Ошибка. Сервер не найден
        /// </summary>
        private static IErrorResult ServerNotFoundError =>
             new ErrorResult(ErrorResultType.ServerNotFound, "Сервер не найден");

        /// <summary>
        /// Преобразование в json
        /// </summary>
        private static StringContent ToStringContent(string jsonContent) =>
            new StringContent(jsonContent, Encoding.UTF8, "application/json");
    }
}