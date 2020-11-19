using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
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
    /// Базовый контроллер для Api. Отправка данных. Тесты
    /// </summary>
    public class ApiControllerPostTest
    {
        /// <summary>
        /// Записать модель в базу. Корректный вариант
        /// </summary>
        [Fact]
        public async Task Post_Value_Ok()
        {
            var testDomain = TestData.TestResultDomain;
            var testDomains = TestData.TestResultDomains;
            var testDomainsId = testDomain.Value.Id;
            var testService = DatabaseServicePostMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = new TestTransferConverter();
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfer = testTransferConverter.ToTransfer(testDomain.Value);
            var actionResult = await testController.Post(testTransfer);

            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var createdAtActionResult = (CreatedAtActionResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
            Assert.IsAssignableFrom<TestEnum>(createdAtActionResult.RouteValues.Values.First());
            var testId = (TestEnum)createdAtActionResult.RouteValues.Values.First();
            Assert.True(testDomainsId.Equals(testId));
        }

        /// <summary>
        /// Записать модель в базу. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task Post_Value_ErrorDatabase()
        {
            var testDomain = TestData.TestDomains.First();
            var initialError = ErrorData.DatabaseError;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testService = DatabaseServicePostMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = new TestTransferConverter();
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfer = testTransferConverter.ToTransfer(testDomain);
            var actionResult = await testController.Post(testTransfer);

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
        public async Task PostValue_NotFound()
        {
            var testDomains = TestData.TestResultDomains;
            var testPost = testDomains.Value.Last();
            var testService = DatabaseServicePostMock.GetTestDatabaseTable(testDomains, DatabaseServicePostMock.PostValueFoundFunc());
            var testTransferConverter = new TestTransferConverter();
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfer = testTransferConverter.ToTransfer(testPost);
            var actionResult = await testController.Post(testTransfer);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Записать модели в базу. Корректный вариант
        /// </summary>
        [Fact]
        public async Task Post_Collection_Ok()
        {
            var testDomains = TestData.TestResultDomains;
            var testDomainsIds = TestData.GetTestIds(testDomains.Value);
            var testService = DatabaseServicePostMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = new TestTransferConverter();
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfers = testTransferConverter.ToTransfers(testDomains.Value).ToList();
            var actionResult = await testController.Post(testTransfers);

            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var createdAtActionResult = (CreatedAtActionResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<TestEnum>>(createdAtActionResult.RouteValues.Values.First());
            var testIds = (IEnumerable<TestEnum>)createdAtActionResult.RouteValues.Values.First();
            Assert.True(testDomainsIds.SequenceEqual(testIds));
        }

        /// <summary>
        /// Записать модели в базу. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task Post_Collection_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testService = DatabaseServicePostMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = new TestTransferConverter();
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfers = testTransferConverter.ToTransfers(testDomains.Value).ToList();
            var actionResult = await testController.Post(testTransfers);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }
    }
}