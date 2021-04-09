using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.Json.Async;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.RestResponses;
using BoutiqueDTO.Models.Interfaces.Base;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Extensions.RestResponses.Async
{
    /// <summary>
    /// Методы расширения ответа Api сервера
    /// </summary>
    public static class RestResponseAsyncExtensions
    {
        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ со значением
        /// </summary>
        public static async Task<IResultValue<TValue>> ToRestResultValueAsync<TValue>(this HttpResponseMessage @this)
            where TValue : notnull =>
            @this.IsSuccessStatusCode 
                ? await @this.Content.ReadAsStringAsync().ToTransferValueJsonAsync<TValue>()
                : RestStatusError.RestStatusToErrorResult(@this).ToResultValue<TValue>();

        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ с коллекцией
        /// </summary>
        public static async Task<IResultCollection<TValue>> ToRestResultCollectionAsync<TValue>(this HttpResponseMessage @this)
            where TValue : notnull =>
              @this.IsSuccessStatusCode
                ? await @this.Content.ReadAsStringAsync().ToTransferCollectionJsonAsync<TValue>()
                : RestStatusError.RestStatusToErrorResult(@this).ToResultCollection<TValue>();
    }
}