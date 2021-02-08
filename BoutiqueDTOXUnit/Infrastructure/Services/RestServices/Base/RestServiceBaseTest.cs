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
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Authorization;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueDTOXUnit.Data;
using BoutiqueDTOXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Services.Implementations.Converters;
using BoutiqueDTOXUnit.Data.Services.Implementations.Services.Api;
using BoutiqueDTOXUnit.Data.Services.Implementations.Services.RestService;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Converters;
using BoutiqueDTOXUnit.Data.Services.Interfaces.Services.Api;
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
        /// Загрузка данных
        /// </summary>
        [Fact]
        public async Task Upload_Ok()
        {
            var tests = TestData.TestDomains;
            var testsIds = tests.Select(test => test.Id);
            var resultIds = new ResultCollection<TestEnum>(testsIds);
            var testApiServicePost = TestApiServiceMock.GetTestApiServicePost(resultIds);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(testApiServicePost.Object, testTransferConverter);

            var result = await testRestService.Post(tests);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Загрузка данных. Ошибка
        /// </summary>
        [Fact]
        public async Task Upload_Error()
        {
            var tests = TestData.TestDomains;
            var error = ErrorTransferData.ErrorBadRequest;
            var resultIds = new ResultCollection<TestEnum>(error);
            var testApiServicePost = TestApiServiceMock.GetTestApiServicePost(resultIds);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(testApiServicePost.Object, testTransferConverter);

            var result = await testRestService.Post(tests);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Удаление данных
        /// </summary>
        [Fact]
        public async Task Delete_Ok()
        {
            var resultDelete = new ResultError();
            var testApiServicePost = TestApiServiceMock.GetTestApiServiceDelete(resultDelete);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(testApiServicePost.Object, testTransferConverter);

            var result = await testRestService.Delete();

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Удаление данных. Ошибка
        /// </summary>
        [Fact]
        public async Task Delete_Error()
        {
            var error = ErrorTransferData.ErrorBadRequest;
            var resultDelete = new ResultError(error);
            var testApiServicePost = TestApiServiceMock.GetTestApiServiceDelete(resultDelete);
            var testTransferConverter = TestTransferConverter;
            var testRestService = new TestRestService(testApiServicePost.Object, testTransferConverter);

            var result = await testRestService.Delete();

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().ErrorResultType == ErrorResultType.BadRequest);
        }

        /// <summary>
        /// Тестовый конвертер трансферных моделей
        /// </summary>
        private static ITestTransferConverter TestTransferConverter =>
            TestTransferConverterMock.TestTransferConverter;

        /// <summary>
        /// Отображение сообщений
        /// </summary>
        private static Mock<IBoutiqueLogger> BoutiqueLogger =>
           new();
    }
}