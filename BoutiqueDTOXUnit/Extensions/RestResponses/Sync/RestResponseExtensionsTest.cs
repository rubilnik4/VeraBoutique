using System.Collections.Generic;
using System.Linq;
using System.Net;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using RestSharp;
using Xunit;

namespace BoutiqueDTOXUnit.Extensions.RestResponses.Sync
{
    /// <summary>
    /// Методы расширения ответа Api сервера. Тесты
    /// </summary>
    public class RestResponseExtensionsTest
    {
        /// <summary>
        /// Преобразование в результирующий ответ
        /// </summary>
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.Created)]
        public void ToRestResultValue(HttpStatusCode httpStatusCode)
        {
            const string value = "test";
            var restResult = new RestResponse<string>
            {
                Data = value,
                StatusCode = httpStatusCode,
            };

            var resultValue = restResult.ToRestResultValue();

            Assert.True(resultValue.OkStatus);
            Assert.Equal(value, resultValue.Value);
        }

        /// <summary>
        /// Преобразование в результирующий ответ. Ошибка
        /// </summary>
        [Fact]
        public void ToRestResultValue_Error()
        {
            const string value = "test";
            var restResult = new RestResponse<string>
            {
                Data = value,
                StatusCode = HttpStatusCode.InternalServerError,
            };

            var resultValue = restResult.ToRestResultValue();

            Assert.True(resultValue.HasErrors);
            Assert.Equal(ErrorResultType.InternalServerError, resultValue.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Преобразование в результирующий ответ
        /// </summary>
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.Created)]
        public void ToRestResultCollection(HttpStatusCode httpStatusCode)
        {
            var values = new List<string> { "First", "Second" };
            var restResult = new RestResponse<List<string>>
            {
                Data = values,
                StatusCode = httpStatusCode,
            };

            var resultCollection = restResult.ToRestResultCollection();

            Assert.True(resultCollection.OkStatus);
            Assert.True(values.SequenceEqual(resultCollection.Value));
        }

        /// <summary>
        /// Преобразование в результирующий ответ. Ошибка
        /// </summary>
        [Fact]
        public void ToRestResultCollection_Error()
        {
            var values = new List<string> { "First", "Second" };
            var restResult = new RestResponse<List<string>>
            {
                Data = values,
                StatusCode = HttpStatusCode.InternalServerError,
            };

            var resultCollection = restResult.ToRestResultCollection();

            Assert.True(resultCollection.HasErrors);
            Assert.Equal(ErrorResultType.InternalServerError, resultCollection.Errors.First().ErrorResultType);
        }

        /// <summary>
        /// Преобразование в результирующий ответ
        /// </summary>
        [Fact]
        public void ToRestResultError()
        {
            var restResult = new RestResponse
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
            var restResult = new RestResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
            };

            var resultCollection = restResult.ToRestResultError();

            Assert.True(resultCollection.HasErrors);
            Assert.Equal(ErrorResultType.InternalServerError, resultCollection.Errors.First().ErrorResultType);
        }
    }
}