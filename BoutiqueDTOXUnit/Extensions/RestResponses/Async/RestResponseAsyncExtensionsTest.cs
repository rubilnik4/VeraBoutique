using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BoutiqueDTO.Extensions.RestResponses.Async;
using BoutiqueDTO.Extensions.RestResponses.Sync;
using Functional.Models.Enums;
using RestSharp;
using Xunit;

namespace BoutiqueDTOXUnit.Extensions.RestResponses.Async
{
    /// <summary>
    /// Асинхронные методы расширения ответа Api сервера. Тесты
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
            var restResult = new RestResponse<string>()
            {
                Data = value,
                StatusCode = httpStatusCode,
            };
            var restResultTask = GetRestResponseTask(restResult);

            var resultValue = await restResultTask.ToRestResultValueAsync();

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
            var restResult = new RestResponse<string>()
            {
                Data = value,
                StatusCode = HttpStatusCode.InternalServerError,
            };
            var restResultTask = GetRestResponseTask(restResult);
            
            var resultValue = await restResultTask.ToRestResultValueAsync();

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
            var restResult = new RestResponse<List<string>>()
            {
                Data = values,
                StatusCode = httpStatusCode,
            };
            var restResultTask =  GetRestResponseTask(restResult);
            
            var resultCollection = await restResultTask.ToRestResultCollectionAsync();

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
            var restResult = new RestResponse<List<string>>()
            {
                Data = values,
                StatusCode = HttpStatusCode.InternalServerError,
            };
            var restResultTask = GetRestResponseTask(restResult);
            
            var resultCollection = await restResultTask.ToRestResultCollectionAsync();

            Assert.True(resultCollection.HasErrors);
            Assert.Equal(ErrorResultType.InternalServerError, resultCollection.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Получить задачу с ответом сервера
        /// </summary>
        private static Task<IRestResponse<TValue>> GetRestResponseTask<TValue>(IRestResponse<TValue> restResponse)
            where TValue : notnull =>
            Task.FromResult(restResponse);
    }
}