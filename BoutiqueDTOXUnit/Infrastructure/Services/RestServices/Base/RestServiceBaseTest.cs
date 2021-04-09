using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Authorization;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Authorization;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Authorization;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Services.Implementations.Converters;
using BoutiqueDTOXUnit.Data.Services.Implementations.Services.RestService;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Converters;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Services;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.RestServices.Base
{
    /// <summary>
    /// Базовый сервис для данных Api. Тесты
    /// </summary>
    public class RestServiceBaseTest
    {
        /// <summary>
        /// Получение данных
        /// </summary>
        [Fact]
        public async Task GetAsync_Ok()
        {
            var tests = TestTransferData.TestTransfers;
            var resultTests = new ResultCollection<TestTransfer>(tests);
            var restHttpClient = RestClientMock.GetRestClient(resultTests);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restHttpClient.Object, testTransferConverter);

            var result = await testRestService.GetAsync();

            Assert.True(result.OkStatus);
            Assert.True(TestData.TestDomains.SequenceEqual(result.Value));
        }

        /// <summary>
        /// Получение данных
        /// </summary>
        [Fact]
        public async Task GetAsync_Error()
        {
            var error = ErrorTransferData.ErrorBadRequest;
            var resultTests = new ResultCollection<TestTransfer>(error);
            var restHttpClient = RestClientMock.GetRestClient(resultTests);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restHttpClient.Object, testTransferConverter);

            var result = await testRestService.GetAsync();

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Получение данных по идентификатору
        /// </summary>
        [Fact]
        public async Task GetByIdAsync_Ok()
        {
            var test = TestTransferData.TestTransfers.First();
            var resultTest = new ResultValue<TestTransfer>(test);
            var testApiServiceGet = RestClientMock.GetRestClient(resultTest);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(testApiServiceGet.Object, testTransferConverter);

            var result = await testRestService.GetAsync(test.Id);

            Assert.True(result.OkStatus);
            Assert.True(TestData.TestDomains.First().Equals(result.Value));
        }

        /// <summary>
        /// Получение данных по идентификатору
        /// </summary>
        [Fact]
        public async Task GetByIdAsync_Error()
        {
            var test = TestTransferData.TestTransfers.First();
            var error = ErrorTransferData.ErrorBadRequest;
            var resultTest = new ResultValue<TestTransfer>(error);
            var testApiServiceGet = RestClientMock.GetRestClient(resultTest);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(testApiServiceGet.Object, testTransferConverter);

            var result = await testRestService.GetAsync(test.Id);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Загрузка данных
        /// </summary>
        [Fact]
        public async Task PostAsync_Ok()
        {
            var tests = TestData.TestDomains;
            var testsIds = tests.Select(test => test.Id);
            var resultIds = new ResultCollection<TestEnum>(testsIds);
            var testApiServicePost = TestApiServiceMock.GetTestApiServicePost(resultIds);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(testApiServicePost.Object, testTransferConverter);

            var result = await testRestService.PostAsync(tests);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Загрузка данных. Ошибка
        /// </summary>
        [Fact]
        public async Task UploadAsync_Error()
        {
            var tests = TestData.TestDomains;
            var error = ErrorTransferData.ErrorBadRequest;
            var resultIds = new ResultCollection<TestEnum>(error);
            var testApiServicePost = TestApiServiceMock.GetTestApiServicePost(resultIds);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(testApiServicePost.Object, testTransferConverter);

            var result = await testRestService.PostAsync(tests);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Удаление данных
        /// </summary>
        [Fact]
        public async Task DeleteAsync_Ok()
        {
            var resultDelete = new ResultError();
            var testApiServicePost = TestApiServiceMock.GetTestApiServiceDelete(resultDelete);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(testApiServicePost.Object, testTransferConverter);

            var result = await testRestService.DeleteAsync();

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Удаление данных. Ошибка
        /// </summary>
        [Fact]
        public async Task DeleteAsync_Error()
        {
            var error = ErrorTransferData.ErrorBadRequest;
            var resultDelete = new ResultError(error);
            var testApiServicePost = TestApiServiceMock.GetTestApiServiceDelete(resultDelete);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(testApiServicePost.Object, testTransferConverter);

            var result = await testRestService.DeleteAsync();

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Тестовый конвертер трансферных моделей
        /// </summary>
        private static ITestTransferConverter TestTransferConverter =>
            TestTransferConverterMock.TestTransferConverter;
    }
}