using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.Json.Sync;
using BoutiqueDTO.Extensions.RestResponses.Async;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDTOXUnit.Extensions.RestResponses.Async
{
    /// <summary>
    /// Асинхронные методы расширения ответа Api сервера. Тесты
    /// </summary>
    public class RestResponseTaskAsyncExtensionsTest
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
                Content = new StringContent(value),
                StatusCode = httpStatusCode,
            };
            var restResultTask = GetRestResponseTask(restResult);

            var resultValue = await restResultTask.ToRestResultValueTaskAsync<string>();

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
            var restResultTask = GetRestResponseTask(restResult);
            
            var resultValue = await restResultTask.ToRestResultValueTaskAsync<string>();

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
            var restResultTask =  GetRestResponseTask(restResult);
            
            var resultCollection = await restResultTask.ToRestResultCollectionTaskAsync<string>();

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
            var restResultTask = GetRestResponseTask(restResult);
            
            var resultCollection = await restResultTask.ToRestResultCollectionTaskAsync<string>();

            Assert.True(resultCollection.HasErrors);
            Assert.Equal(ErrorResultType.InternalServerError, resultCollection.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Преобразование в результирующий ответ
        /// </summary>
        [Fact]
        public async Task ToRestResultError()
        {
            var restResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent,
            };
            var restResultTask = GetRestResponseTask(restResult);

            var resultCollection = await restResultTask.ToRestResultErrorTaskAsync();

            Assert.True(resultCollection.OkStatus);
        }

        /// <summary>
        /// Преобразование в результирующий ответ. Ошибка
        /// </summary>
        [Fact]
        public async Task ToRestResultError_Error()
        {
            var restResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
            };
            var restResultTask = GetRestResponseTask(restResult);

            var resultCollection = await restResultTask.ToRestResultErrorTaskAsync();

            Assert.True(resultCollection.HasErrors);
            Assert.Equal(ErrorResultType.InternalServerError, resultCollection.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Получить задачу с ответом сервера
        /// </summary>
        private static Task<HttpResponseMessage> GetRestResponseTask(HttpResponseMessage httpResponseMessage) =>
            Task.FromResult(httpResponseMessage);
    }
}