using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Models.Enums.RestClients;
using BoutiqueDTO.Models.Interfaces.RestClients;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Models.Implementations.RestClients
{
    /// <summary>
    /// Клиент для http запросов
    /// </summary>
    public class RestHttpClient: IRestHttpClient
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
            await _httpClient.GetAsync(request).
            ToRestResultValueTaskAsync<TOut>();

        /// <summary>
        /// Получить байтовый массив по идентификатору Api
        /// </summary>
        public async Task<IResultValue<byte[]>> GetByteAsync(string request) =>
            await ResultValueTryAsyncExtensions.ResultValueTryAsync(() => _httpClient.GetByteArrayAsync(request),
                                                                    new ErrorResult(ErrorResultType.BadRequest, "Невозможно загрузить байтовый массив"));

        /// <summary>
        /// Получить данные Api
        /// </summary>
        public async Task<IResultCollection<TOut>> GetCollectionAsync<TOut>(string request)
            where TOut : notnull =>
             await _httpClient.GetAsync(request).
             ToRestResultCollectionTaskAsync<TOut>();

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        public async Task<IResultValue<string>> PostAsync(string request, string jsonContent) =>
            await _httpClient.PostAsync(request, new StringContent(jsonContent, Encoding.UTF8, "application/json")).
            ToRestResultTaskAsync();

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        public async Task<IResultValue<TOut>> PostValueAsync<TOut>(string request, string jsonContent)
            where TOut : notnull =>
            await _httpClient.PostAsync(request, new StringContent(jsonContent, Encoding.UTF8, "application/json")).
            ToRestResultValueTaskAsync<TOut>();

        /// <summary>
        /// Добавить коллекцию данных Api
        /// </summary>
        public async Task<IResultCollection<TOut>> PostCollectionAsync<TOut>(string request, string jsonContent) 
            where TOut : notnull =>
            await _httpClient.PostAsync(request, new StringContent(jsonContent, Encoding.UTF8, "application/json")).
            ToRestResultCollectionTaskAsync<TOut>();

        /// <summary>
        /// Обновить данные Api по идентификатору
        /// </summary>
        public async Task<IResultError> PutValueAsync(string request, string jsonContent) =>
            await _httpClient.PutAsync(request, new StringContent(jsonContent, Encoding.UTF8, "application/json")).
            ToRestResultErrorTaskAsync();

        /// <summary>
        /// Удалить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TOut>> DeleteValueAsync<TOut>(string request)
            where TOut : notnull =>
            await _httpClient.DeleteAsync(request).
            ToRestResultValueTaskAsync<TOut>();

        /// <summary>
        /// Удалить данные Api
        /// </summary>
        public async Task<IResultError> DeleteCollectionAsync(string request) =>
            await _httpClient.DeleteAsync(request).
            ToRestResultErrorTaskAsync();
    }
}