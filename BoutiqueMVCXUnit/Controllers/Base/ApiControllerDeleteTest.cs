using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data.Services.Implementations;
using BoutiqueDTOXUnit.Data.Services.Interfaces;
using BoutiqueDTOXUnit.Data.Services.Mocks.Converters;
using BoutiqueMVCXUnit.Controllers.Base.Mocks;
using BoutiqueMVCXUnit.Data.Controllers.Implementations;
using Functional.Models.Implementations.Result;
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
        /// Изменить модель в базе. Корректный вариант
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
            var testsAfter = testTransferConverter.FromTransfer(actionResult.Value);

            Assert.True(testsAfter.Value.Equals(testDelete));
        }

        /// <summary>
        /// Изменить модель в базе. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task Delete_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
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
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Изменить модель в базе. Элемент не найден
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