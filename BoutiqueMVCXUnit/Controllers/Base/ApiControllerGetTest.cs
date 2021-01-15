using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data.Services.Implementations;
using BoutiqueDTOXUnit.Data.Services.Interfaces;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using BoutiqueMVCXUnit.Controllers.Base.Mocks;
using BoutiqueMVCXUnit.Data.Controllers.Implementations;
using Functional.Models.Implementations.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BoutiqueMVCXUnit.Controllers.Base
{
    /// <summary>
    /// Базовый контроллер для Api. Получение данных. Тесты
    /// </summary>
    public class ApiControllerGetTest
    {
        /// <summary>
        /// Получить модели из базы. Корректный вариант
        /// </summary>
        [Fact]
        public async Task Get_Ok()
        {
            var testDomains = TestData.TestResultDomains;
            var testService = DatabaseServiceGetMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var actionResult = await testController.Get();
            var testsAfter = testTransferConverter.FromTransfers(actionResult.Value);

            Assert.True(testsAfter.Value.SequenceEqual(testDomains.Value));
        }

        /// <summary>
        /// Получить модели из базы. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task Get_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testService = DatabaseServiceGetMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var actionResult = await testController.Get();

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Получить модель из базы по идентификатору. Корректный вариант
        /// </summary>
        [Fact]
        public async Task GetById_Ok()
        {
            var testDomains = TestData.TestResultDomains;
            var testGet = testDomains.Value.Last();
            var testGetId = testGet.Id;
            var testService = DatabaseServiceGetMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var actionResult = await testController.Get(testGetId);
            var testAfter = testTransferConverter.FromTransfer(actionResult.Value);

            Assert.True(testAfter.Value.Equals(testGet));
        }

        /// <summary>
        /// Получить модель из базы по идентификатору. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task GetById_ErrorDatabase()
        {
            var testGetId = TestData.TestDomains.Last().Id;
            var initialError = ErrorData.DatabaseError;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testService = DatabaseServiceGetMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var actionResult = await testController.Get(testGetId);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Получить модель из базы по идентификатору. Элемент не найден
        /// </summary>
        [Fact]
        public async Task GetById_NotFound()
        {
            var testGetId = TestData.TestDomains.Last().Id;
            var initialError = ErrorData.DatabaseError;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testService = DatabaseServiceGetMock.GetTestDatabaseTable(testDomains, DatabaseServiceGetMock.GetByIdNotFoundFunc());
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var actionResult = await testController.Get(testGetId);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
    }
}