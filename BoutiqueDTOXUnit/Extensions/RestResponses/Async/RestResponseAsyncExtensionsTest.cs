using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Extensions.RestResponses.Sync;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDTOXUnit.Extensions.RestResponses.Async
{
    /// <summary>
    /// Методы расширения ответа Api сервера. Тесты
    /// </summary>
    public class RestResponseAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразование в результирующий ответ
        /// </summary>
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.Created)]
        public async Task ToRestResultValue(HttpStatusCode httpStatusCode)
        {
            const string value = "test";
            var restResult = new HttpResponseMessage
            {
                Content = new StringContent(value.ToJsonTransfer().Value),
                StatusCode = httpStatusCode,
            };

            var resultValue = await restResult.ToRestResultValueAsync<string>();

            Assert.True(resultValue.OkStatus);
            Assert.Equal(value, resultValue.Value);
        }

        /// <summary>
        /// Преобразование в результирующий ответ. Ошибка
        /// </summary>
        [Fact]
        public async Task ToRestResultValue_Error()
        {
            const string value = "test";
            var restResult = new HttpResponseMessage
            {
                Content = new StringContent(value),
                StatusCode = HttpStatusCode.InternalServerError,
            };

            var resultValue = await restResult.ToRestResultValueAsync<string>();

            Assert.True(resultValue.HasErrors);
            Assert.Equal(ErrorResultType.InternalServerError, resultValue.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Преобразование в результирующий ответ
        /// </summary>
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.Created)]
        public async Task ToRestResultCollection(HttpStatusCode httpStatusCode)
        {
            var values = new List<string> { "First", "Second" };
            var restResult = new HttpResponseMessage
            {
                Content = new StringContent(values.ToJsonTransfer().Value),
                StatusCode = httpStatusCode,
            };

            var resultCollection = await restResult.ToRestResultCollectionAsync<string>();

            Assert.True(resultCollection.OkStatus);
            Assert.True(values.SequenceEqual(resultCollection.Value));
        }

        /// <summary>
        /// Преобразование в результирующий ответ. Ошибка
        /// </summary>
        [Fact]
        public async Task ToRestResultCollection_Error()
        {
            var values = new List<string> { "First", "Second" };
            var restResult = new HttpResponseMessage
            {
                Content = new StringContent(values.ToJsonTransfer().Value),
                StatusCode = HttpStatusCode.InternalServerError,
            };

            var resultCollection = await restResult.ToRestResultCollectionAsync<string>();

            Assert.True(resultCollection.HasErrors);
            Assert.Equal(ErrorResultType.InternalServerError, resultCollection.Errors.First().ErrorResultType);
        }
    }
}