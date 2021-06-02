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
            var restClient = RestClientMock.GetRestClient(resultTests);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

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
            var restClient = RestClientMock.GetRestClient(resultTests);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

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
            var restClient = RestClientMock.GetRestClient(resultTest);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

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
            var restClient = RestClientMock.GetRestClient(resultTest);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.GetAsync(test.Id);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Загрузка данных
        /// </summary>
        [Fact]
        public async Task PostCollectionAsync_Ok()
        {
            var tests = TestData.TestDomains;
            var testsIds = tests.Select(test => test.Id).ToList();
            var resultIds = new ResultCollection<TestEnum>(testsIds);
            var restClient = RestClientMock.PostRestClient(resultIds);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.PostAsync(tests);

            Assert.True(result.OkStatus);
            Assert.True(result.Value.SequenceEqual(testsIds));
        }

        /// <summary>
        /// Загрузка данных. Ошибка
        /// </summary>
        [Fact]
        public async Task PostCollectionAsync_Error()
        {
            var tests = TestData.TestDomains;
            var error = ErrorTransferData.ErrorBadRequest;
            var resultIds = new ResultCollection<TestEnum>(error);
            var restClient = RestClientMock.PostRestClient(resultIds);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.PostAsync(tests);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Загрузка данных
        /// </summary>
        [Fact]
        public async Task PostValueAsync_Ok()
        {
            var test = TestData.TestDomains.First();
            var testId = test.Id;
            var resultId = new ResultValue<TestEnum>(testId);
            var restClient = RestClientMock.PostRestClient(resultId);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.PostAsync(test);

            Assert.True(result.OkStatus);
            Assert.True(result.Value.Equals(testId));
        }

        /// <summary>
        /// Загрузка данных. Ошибка
        /// </summary>
        [Fact]
        public async Task PostValueAsync_Error()
        {
            var test = TestData.TestDomains.First();
            var error = ErrorTransferData.ErrorBadRequest;
            var resultIds = new ResultValue<TestEnum>(error);
            var restClient = RestClientMock.PostRestClient(resultIds);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.PostAsync(test);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Обновление данных
        /// </summary>
        [Fact]
        public async Task PutAsync_Ok()
        {
            var testDomain = TestData.TestDomains.First();
            var test = TestTransferData.TestTransfers.First();
            var resultTest = new ResultValue<TestTransfer>(test);
            var restClient = RestClientMock.PutRestClient(resultTest);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.PutAsync(testDomain);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Обновление данных. Ошибка
        /// </summary>
        [Fact]
        public async Task PutAsync_Error()
        {
            var testDomain = TestData.TestDomains.First();
            var error = ErrorTransferData.ErrorBadRequest;
            var resultTest = new ResultError(error);
            var restClient = RestClientMock.PutRestClient(resultTest);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.PutAsync(testDomain);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Удаление данных
        /// </summary>
        [Fact]
        public async Task DeleteAsync_Ok()
        {
            var resultTest = new ResultError();
            var restClient = RestClientMock.DeleteRestClient(resultTest);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

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
            var resultTest = new ResultError(error);
            var restClient = RestClientMock.DeleteRestClient(resultTest);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.DeleteAsync();

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Удаление данных
        /// </summary>
        [Fact]
        public async Task DeleteByIdAsync_Ok()
        {
            var test = TestTransferData.TestTransfers.First();
            var resultTest = new ResultValue<TestTransfer>(test);
            var restClient = RestClientMock.DeleteRestClient(resultTest);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.DeleteAsync(test.Id);

            Assert.True(result.OkStatus);
            Assert.True(result.Value.Equals(test));
        }

        /// <summary>
        /// Удаление данных. Ошибка
        /// </summary>
        [Fact]
        public async Task DeleteByIdAsync_Error()
        {
            var test = TestTransferData.TestTransfers.First();
            var error = ErrorTransferData.ErrorBadRequest;
            var resultTest = new ResultValue<TestTransfer>(error);
            var restClient = RestClientMock.DeleteRestClient(resultTest);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.DeleteAsync(test.Id);

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