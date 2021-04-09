using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using BoutiqueDTO.Extensions.RestResponses.Sync;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Xunit;

namespace BoutiqueDTOXUnit.Extensions.RestResponses.Sync
{
    /// <summary>
    /// Методы расширения ответа Api сервера. Тесты
    /// </summary>
    public class RestResponseAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразование в результирующий ответ
        /// </summary>
        [Fact]
        public void ToRestResultError()
        {
            var restResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent,
            };

            var resultCollection = restResult.ToRestResultError();

            Assert.True(resultCollection.OkStatus);
        }

        /// <summary>
        /// Преобразование в результирующий ответ. Ошибка
        /// </summary>
        [Fact]
        public void ToRestResultError_Error()
        {
            var restResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
            };

            var resultCollection = restResult.ToRestResultError();

            Assert.True(resultCollection.HasErrors);
            Assert.Equal(ErrorResultType.InternalServerError, resultCollection.Errors.First().ErrorResultType);
        }
    }
}