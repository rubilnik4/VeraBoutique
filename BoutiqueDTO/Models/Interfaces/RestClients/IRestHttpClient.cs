using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.RestResponses.Async;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Models.Interfaces.RestClients
{
    /// <summary>
    /// Клиент для http запросов
    /// </summary>
    public interface IRestHttpClient
    {
        /// <summary>
        /// Получить данные по идентификатору Api
        /// </summary>
         Task<IResultValue<TOut>> GetValueAsync<TOut>(string request)
            where TOut : notnull;

        /// <summary>
        /// Получить данные Api
        /// </summary>
        Task<IResultCollection<TOut>> GetCollectionAsync<TOut>(string request) 
            where TOut : notnull;

        /// <summary>
        /// Добавить данные Api
        /// </summary>
        Task<IResultValue<TOut>> PostValueAsync<TOut>(string request, string jsonContent) 
            where TOut : notnull;

        /// <summary>
        /// Добавить коллекцию данных Api
        /// </summary>
        Task<IResultCollection<TOut>> PostCollectionAsync<TOut>(string request, string jsonContent)
            where TOut : notnull;

        /// <summary>
        /// Обновить данные Api по идентификатору
        /// </summary>
        Task<IResultError> PutValueAsync<TOut>(string request, string jsonContent)
            where TOut : notnull;

        /// <summary>
        /// Удалить данные по идентификатору Api
        /// </summary>
        Task<IResultValue<TOut>> DeleteValueAsync<TOut>(string request)
            where TOut : notnull;

        /// <summary>
        /// Удалить данные Api
        /// </summary>
        Task<IResultCollection<TOut>> DeleteCollectionAsync<TOut>(string request)
            where TOut : notnull;
    }
}