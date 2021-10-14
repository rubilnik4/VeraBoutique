using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data.Services.Implementations;
using BoutiqueDTOXUnit.Data.Services.Interfaces;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using BoutiqueMVCXUnit.Controllers.Base.Mocks;
using BoutiqueMVCXUnit.Data.Controllers.Implementations;
using ResultFunctional.Models.Implementations.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BoutiqueMVCXUnit.Controllers.Base
{
    /// <summary>
    /// Базовый контроллер для Api. Удаление данных. Тесты
    /// </summary>
    public class ApiControllerDeleteTest
    {
        /// <summary>
        /// Удалить все модели в базе. Корректный вариант
        /// </summary>
        [Fact]
        public async Task DeleteAll_Ok()
        {
            var testDomains = TestData.TestResultDomains;
            var testService = DatabaseServiceDeleteMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var actionResult = await testController.Delete();

            Assert.IsType<NoContentResult>(actionResult);
            var noContentResult = (NoContentResult)actionResult;
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        /// <summary>
        /// Удалить все модели в базе. Корректный вариант
        /// </summary>
        [Fact]
        public async Task DeleteAll_ErrorDatabase()
        {
            var initialError = ErrorData.ErrorTest;
            var testResult = new ResultError(initialError);
            var testDomains = TestData.TestResultDomains;
            var testService = DatabaseServiceDeleteMock.GetTestDatabaseTable(testDomains, testResult);
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var actionResult = await testController.Delete();

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.Id, errors.Keys.First());
        }

        /// <summary>
        /// Удалить модель в базе. Корректный вариант
        /// </summary>
        [Fact]
        public async Task Delete_Ok()
        {
            var testDomains = TestData.TestResultDomains;
            var testDelete = testDomains.Value.Last();
            var testDeleteId = testDelete.Id;
            var testService = DatabaseServiceDeleteMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var actionResult = await testController.Delete(testDeleteId);

            Assert.Equal(testDeleteId, actionResult.Value);
        }

        /// <summary>
        /// Удалить модель в базе. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task Delete_ErrorDatabase()
        {
            var initialError = ErrorData.ErrorTest;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testDelete = TestData.TestDomains.Last();
            var testDeleteId = testDelete.Id;
            var testService = DatabaseServiceDeleteMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var actionResult = await testController.Delete(testDeleteId);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.Id, errors.Keys.First());
        }

        /// <summary>
        /// Удалить модель в базе. Элемент не найден
        /// </summary>
        [Fact]
        public async Task Delete_NotFound()
        {
            var testDomains = TestData.TestResultDomains;
            var testDelete = testDomains.Value.Last();
            var testDeleteId = testDelete.Id;
            var testService = DatabaseServiceDeleteMock.GetTestDatabaseTable(testDomains, DatabaseServiceDeleteMock.DeleteNotFoundFunc());
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var actionResult = await testController.Delete(testDeleteId);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
    }
}