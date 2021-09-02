using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.Json.Async;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.RestResponses;
using BoutiqueDTO.Models.Interfaces.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Extensions.RestResponses.Sync
{
    /// <summary>
    /// Методы расширения ответа Api сервера
    /// </summary>
    public static class RestResponseExtensions
    {
        /// <summary>
        /// Преобразовать ответ сервера в результирующий ответ
        /// </summary>
        public static IResultError ToRestResultError(this HttpResponseMessage @this) =>
            @this.IsSuccessStatusCode
                ? new ResultError()
                : RestStatusError.RestStatusToErrorResult(@this).ToResult();
    }
}