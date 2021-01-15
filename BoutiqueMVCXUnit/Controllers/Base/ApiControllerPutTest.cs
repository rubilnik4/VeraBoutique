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
    /// Базовый контроллер для Api. Обновление данных. Тесты
    /// </summary>
    public class ApiControllerPutTest
    {
        /// <summary>
        /// Изменить модель в базе. Корректный вариант
        /// </summary>
        [Fact]
        public async Task Put_Ok()
        {
            var testDomains = TestData.TestResultDomains;
            var testPut = testDomains.Value.Last();
            var testService = DatabaseServicePutMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfer = testTransferConverter.ToTransfer(testPut);
            var actionResult = await testController.Put(testTransfer);

            Assert.IsType<NoContentResult>(actionResult);
            var noContentResult = (NoContentResult)actionResult;
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        /// <summary>
        /// Изменить модель в базе. Ошибка базы
        /// </summary>
        [Fact]
        public async Task Put_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testPut = TestData.TestResultDomains.Value.Last();
            var testService = DatabaseServicePutMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfer = testTransferConverter.ToTransfer(testPut);
            var actionResult = await testController.Put(testTransfer);

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Изменить модель в базе. Элемент не найден
        /// </summary>
        [Fact]
        public async Task Put_NotFound()
        {
            var testDomains = TestData.TestResultDomains;
            var testPut = testDomains.Value.Last();
            var testService = DatabaseServicePutMock.GetTestDatabaseTable(testDomains, DatabaseServicePutMock.PutNotFoundFunc());
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfer = testTransferConverter.ToTransfer(testPut);
            var actionResult = await testController.Put(testTransfer);

            Assert.IsType<NotFoundResult>(actionResult);
            var notFoundResult = (NotFoundResult)actionResult;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
    }
}