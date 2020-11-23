using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Data.Services.Implementations;
using BoutiqueMVCXUnit.Controllers.Base.Mocks;
using BoutiqueMVCXUnit.Data.Controllers.Implementations;
using Functional.Models.Implementations.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BoutiqueMVCXUnit.Controllers.Base
{
    /// <summary>
    /// Базовый контроллер для Api. Проверка данных. Тесты
    /// </summary>
    public class ApiControllerValidateTest
    {
        /// <summary>
        /// Проверить модель в базе. Корректный вариант
        /// </summary>
        [Fact]
        public async Task Validate_Value_Ok()
        {
            var testDomain = TestData.TestResultDomain;
            var testDomains = TestData.TestResultDomains;
            var testService = DatabaseServiceValidateMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = new TestTransferConverter();
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfer = testTransferConverter.ToTransfer(testDomain.Value);
            var actionResult = await testController.Validate(testTransfer);

            Assert.IsType<NoContentResult>(actionResult);
            var noContentResult = (NoContentResult)actionResult;
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        /// <summary>
        /// Проверить модель в базе. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task Validate_Value_ErrorDatabase()
        {
            var testDomain = TestData.TestDomains.First();
            var initialError = ErrorData.DatabaseError;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testService = DatabaseServiceValidateMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = new TestTransferConverter();
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfer = testTransferConverter.ToTransfer(testDomain);
            var actionResult = await testController.Validate(testTransfer);

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Проверить модель в базе. Элемент не найден
        /// </summary>
        [Fact]
        public async Task Validate_NotFound()
        {
            var testDomains = TestData.TestResultDomains;
            var testPost = testDomains.Value.Last();
            var testService = DatabaseServiceValidateMock.GetTestDatabaseTable(testDomains, DatabaseServiceValidateMock.ValidateValueNotFoundFunc());
            var testTransferConverter = new TestTransferConverter();
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfer = testTransferConverter.ToTransfer(testPost);
            var actionResult = await testController.Validate(testTransfer);

            Assert.IsType<NotFoundResult>(actionResult);
            var notFoundResult = (NotFoundResult)actionResult;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Проверить модели в базе. Корректный вариант
        /// </summary>
        [Fact]
        public async Task Validate_Collection_Ok()
        {
            var testDomains = TestData.TestResultDomains;
            var testService = DatabaseServiceValidateMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = new TestTransferConverter();
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfers = testTransferConverter.ToTransfers(testDomains.Value).ToList();
            var actionResult = await testController.Validates(testTransfers);

            Assert.IsType<NoContentResult>(actionResult);
            var noContentResult = (NoContentResult)actionResult;
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        /// <summary>
        /// Проверить модели в базе. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task Validate_Collection_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testService = DatabaseServiceValidateMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = new TestTransferConverter();
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfers = testTransferConverter.ToTransfers(testDomains.Value).ToList();
            var actionResult = await testController.Validates(testTransfers);

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

    }
}