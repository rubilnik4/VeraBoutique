using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.Json.Async;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.RestResponses;
using BoutiqueDTO.Models.Interfaces.Base;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Extensions.RestResponses.Async
{
    /// <summary>
    /// Методы расширения ответа Api сервера
    /// </summary>
    public static class RestResponseAsyncExtensions
    {
        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ со значением строки
        /// </summary>
        public static async Task<IResultValue<string>> ToRestResultAsync(this HttpResponseMessage @this) =>
            @this.IsSuccessStatusCode
                ? (await @this.Content.ReadAsStringAsync()).Trim('"').ToResultValue()
                : (await RestStatusError.RestStatusToErrorResult(@this)).ToResultValue<string>();

        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ со значением
        /// </summary>
        public static async Task<IResultValue<TValue>> ToRestResultValueAsync<TValue>(this HttpResponseMessage @this)
            where TValue : notnull =>
            @this.IsSuccessStatusCode
                ? await @this.Content.ReadAsStringAsync().ToTransferValueJsonAsync<TValue>()
                : (await RestStatusError.RestStatusToErrorResult(@this)).ToResultValue<TValue>();

        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ с коллекцией
        /// </summary>
        public static async Task<IResultCollection<TValue>> ToRestResultCollectionAsync<TValue>(this HttpResponseMessage @this)
            where TValue : notnull =>
              @this.IsSuccessStatusCode
                ? await @this.Content.ReadAsStringAsync().ToTransferCollectionJsonAsync<TValue>()
                : (await RestStatusError.RestStatusToErrorResult(@this)).ToResultCollection<TValue>();
    }
}