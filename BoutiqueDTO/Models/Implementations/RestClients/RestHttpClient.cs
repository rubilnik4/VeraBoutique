using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Models.Interfaces.RestClients;
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
        /// Получить данные по идентификатору Api
        /// </summary>
        public async Task<IResultValue<TOut>> GetValueAsync<TOut>(string request)
            where TOut : notnull =>
            await _httpClient.GetAsync(request).
            ToRestResultValueTaskAsync<TOut>();

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
        public async Task<IResultValue<TOut>> PostValueAsync<TOut>(string request, string jsonContent)
            where TOut : notnull =>
            await _httpClient.PostAsync(request, new StringContent(jsonContent)).
            ToRestResultValueTaskAsync<TOut>();

        /// <summary>
        /// Добавить коллекцию данных Api
        /// </summary>
        public async Task<IResultCollection<TOut>> PostCollectionAsync<TOut>(string request, string jsonContent) 
            where TOut : notnull =>
            await _httpClient.PostAsync(request, new StringContent(jsonContent)).
            ToRestResultCollectionTaskAsync<TOut>();

        /// <summary>
        /// Обновить данные Api по идентификатору
        /// </summary>
        public async Task<IResultError> PutValueAsync<TOut>(string request, string jsonContent)
            where TOut : notnull =>
            await _httpClient.PutAsync(request, new StringContent(jsonContent)).
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
        public async Task<IResultCollection<TOut>> DeleteCollectionAsync<TOut>(string request)
            where TOut : notnull =>
            await _httpClient.DeleteAsync(request).
            ToRestResultCollectionTaskAsync<TOut>();
    }
}