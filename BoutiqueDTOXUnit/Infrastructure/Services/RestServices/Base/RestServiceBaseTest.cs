using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTO.Models.Implementations.RestClients;
using BoutiqueDTO.Models.Interfaces.RestClients;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Services.Implementations.Converters;
using BoutiqueDTOXUnit.Data.Services.Implementations.Services.RestService;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Converters;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Services;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.RestErrors;
using ResultFunctional.Models.Implementations.Results;
using Moq;
using ResultFunctional.Models.Interfaces.Errors.RestErrors;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Services.RestServices.Base
{
    /// <summary>
    /// Базовый сервис для данных Api. Тесты
    /// </summary>
    public class RestServiceBaseTest
    {
        /// <summary>
        /// Имя контроллера
        /// </summary>
        [Fact]
        public void ControllerName_Ok()
        {
            var tests = TestTransferData.TestTransfers;
            var resultTests = new ResultCollection<TestTransfer>(tests);
            var restClient = RestClientMock.GetRestClient(resultTests);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var controllerName = testRestService.ControllerName;

            Assert.Equal("Test", controllerName);
        }

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
            var error = ErrorTransferData.ErrorTypeBadRequest;
            var resultTests = new ResultCollection<TestTransfer>(error);
            var restClient = RestClientMock.GetRestClient(resultTests);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.GetAsync();

            Assert.True(result.HasErrors);
            Assert.IsType<RestMessageErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.BadRequest, ((RestMessageErrorResult)result.Errors.First()).ErrorType);
        }

        /// <summary>
        /// Получение данных
        /// </summary>
        [Fact]
        public async Task GetAsync_ErrorException()
        {
            var restClient = NotFoundRestHttpClient;
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient, testTransferConverter);

            var result = await testRestService.GetAsync();

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IRestErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.RequestTimeout, ((IRestErrorResult)result.Errors.First()).ErrorType);
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
            var error = ErrorTransferData.ErrorTypeBadRequest;
            var resultTest = new ResultValue<TestTransfer>(error);
            var restClient = RestClientMock.GetRestClient(resultTest);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.GetAsync(test.Id);

            Assert.True(result.HasErrors);
            Assert.IsType<RestMessageErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.BadRequest, ((RestMessageErrorResult)result.Errors.First()).ErrorType);
        }

        /// <summary>
        /// Получение данных по идентификатору
        /// </summary>
        [Fact]
        public async Task GetByIdAsync_ErrorException()
        {
            var test = TestTransferData.TestTransfers.First();
            var restClient = NotFoundRestHttpClient;
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient, testTransferConverter);

            var result = await testRestService.GetAsync(test.Id);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IRestErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.RequestTimeout, ((IRestErrorResult)result.Errors.First()).ErrorType);
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

            var result = await testRestService.PostCollectionAsync(tests);

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
            var error = ErrorTransferData.ErrorTypeBadRequest;
            var resultIds = new ResultCollection<TestEnum>(error);
            var restClient = RestClientMock.PostRestClient(resultIds);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.PostCollectionAsync(tests);

            Assert.True(result.HasErrors);
            Assert.IsType<RestMessageErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.BadRequest, ((RestMessageErrorResult)result.Errors.First()).ErrorType);
        }

        /// <summary>
        /// Загрузка данных. Ошибка
        /// </summary>
        [Fact]
        public async Task PostCollectionAsync_ErrorException()
        {
            var tests = TestData.TestDomains;
            var restClient = NotFoundRestHttpClient;
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient, testTransferConverter);

            var result = await testRestService.PostCollectionAsync(tests);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IRestErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.RequestTimeout, ((IRestErrorResult)result.Errors.First()).ErrorType);
        }

        /// <summary>
        /// Загрузка данных
        /// </summary>
        [Fact]
        public async Task PostAsync_Ok()
        {
            var test = TestData.TestDomains.First();
            var testId = test.Id;
            var resultId = new ResultValue<string>(testId.ToString());
            var restClient = RestClientMock.PostRestClient(resultId);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.PostAsync(test);

            Assert.True(result.OkStatus);
            Assert.True(result.Value.Equals(testId.ToString()));
        }

        /// <summary>
        /// Загрузка данных. Ошибка
        /// </summary>
        [Fact]
        public async Task PostAsync_Error()
        {
            var test = TestData.TestDomains.First();
            var error = ErrorTransferData.ErrorTypeBadRequest;
            var resultIds = new ResultValue<string>(error);
            var restClient = RestClientMock.PostRestClient(resultIds);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.PostAsync(test);

            Assert.True(result.HasErrors);
            Assert.IsType<RestMessageErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.BadRequest, ((RestMessageErrorResult)result.Errors.First()).ErrorType);
        }

        /// <summary>
        /// Загрузка данных. Ошибка
        /// </summary>
        [Fact]
        public async Task PostAsync_ErrorException()
        {
            var test = TestData.TestDomains.First();
            var restClient = NotFoundRestHttpClient;
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient, testTransferConverter);

            var result = await testRestService.PostAsync(test);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IRestErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.RequestTimeout, ((IRestErrorResult)result.Errors.First()).ErrorType);
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

            var result = await testRestService.PostValueAsync(test);

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
            var error = ErrorTransferData.ErrorTypeBadRequest;
            var resultIds = new ResultValue<TestEnum>(error);
            var restClient = RestClientMock.PostRestClient(resultIds);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.PostValueAsync(test);

            Assert.True(result.HasErrors);
            Assert.IsType<RestMessageErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.BadRequest, ((RestMessageErrorResult)result.Errors.First()).ErrorType);
        }

        /// <summary>
        /// Загрузка данных. Ошибка
        /// </summary>
        [Fact]
        public async Task PostValueAsync_ErrorException()
        {
            var test = TestData.TestDomains.First();
            var restClient = NotFoundRestHttpClient;
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient, testTransferConverter);

            var result = await testRestService.PostValueAsync(test);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IRestErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.RequestTimeout, ((IRestErrorResult)result.Errors.First()).ErrorType);
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
            var test = TestData.TestDomains.First();
            var error = ErrorTransferData.ErrorTypeBadRequest;
            var resultTest = new ResultError(error);
            var restClient = RestClientMock.PutRestClient(resultTest);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.PutAsync(test);

            Assert.True(result.HasErrors);
            Assert.IsType<RestMessageErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.BadRequest, ((RestMessageErrorResult)result.Errors.First()).ErrorType);
        }

        /// <summary>
        /// Обновление данных. Ошибка
        /// </summary>
        [Fact]
        public async Task PutAsync_ErrorException()
        {
            var test = TestData.TestDomains.First();
            var restClient = NotFoundRestHttpClient;
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient, testTransferConverter);

            var result = await testRestService.PutAsync(test);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IRestErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.RequestTimeout, ((IRestErrorResult)result.Errors.First()).ErrorType);
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
            var error = ErrorTransferData.ErrorTypeBadRequest;
            var resultTest = new ResultError(error);
            var restClient = RestClientMock.DeleteRestClient(resultTest);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.DeleteAsync();

            Assert.True(result.HasErrors);
            Assert.IsType<RestMessageErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.BadRequest, ((RestMessageErrorResult)result.Errors.First()).ErrorType);
        }

        /// <summary>
        /// Удаление данных. Ошибка
        /// </summary>
        [Fact]
        public async Task DeleteAsync_ErrorException()
        {
            var restClient = NotFoundRestHttpClient;
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient, testTransferConverter);

            var result = await testRestService.DeleteAsync();

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IRestErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.RequestTimeout, ((IRestErrorResult)result.Errors.First()).ErrorType);
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
            var error = ErrorTransferData.ErrorTypeBadRequest;
            var resultTest = new ResultValue<TestTransfer>(error);
            var restClient = RestClientMock.DeleteRestClient(resultTest);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient.Object, testTransferConverter);

            var result = await testRestService.DeleteAsync(test.Id);

            Assert.True(result.HasErrors);
            Assert.IsType<RestMessageErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.BadRequest, ((RestMessageErrorResult)result.Errors.First()).ErrorType);
        }

        /// <summary>
        /// Удаление данных. Ошибка
        /// </summary>
        [Fact]
        public async Task DeleteByIdAsync_ErrorException()
        {
            var test = TestTransferData.TestTransfers.First();
            var restClient = NotFoundRestHttpClient;
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(restClient, testTransferConverter);

            var result = await testRestService.DeleteAsync(test.Id);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IRestErrorResult>(result.Errors.First());
            Assert.Equal(RestErrorType.RequestTimeout, ((IRestErrorResult)result.Errors.First()).ErrorType);
        }


        /// <summary>
        /// Тестовый конвертер трансферных моделей
        /// </summary>
        private static ITestTransferConverter TestTransferConverter =>
            TestTransferConverterMock.TestTransferConverter;

        /// <summary>
        /// Подключение к несуществующему серверу
        /// </summary>
        private static IRestHttpClient NotFoundRestHttpClient =>
             new HttpClient
             {
                 BaseAddress = new Uri("https://ServerNotFound"),
                 Timeout = TimeSpan.FromMilliseconds(10)
             }.
             Map(httpClient => new RestHttpClient(httpClient));
    }
}