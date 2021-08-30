using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data.Services.Implementations;
using BoutiqueDTOXUnit.Data.Services.Interfaces;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using BoutiqueMVCXUnit.Controllers.Base.Mocks;
using BoutiqueMVCXUnit.Data.Controllers.Implementations;
using Functional.Models.Implementations.Results;
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
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfer = testTransferConverter.ToTransfer(testDomain.Value);
            var actionResult = await testController.Post(testTransfer);

            Assert.True(testDomainsId.Equals(actionResult.Value));
        }

        /// <summary>
        /// Записать модель в базу. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task Post_Value_ErrorDatabase()
        {
            var testDomain = TestData.TestDomains.First();
            var initialError = ErrorData.ErrorTest;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testService = DatabaseServicePostMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfer = testTransferConverter.ToTransfer(testDomain);
            var actionResult = await testController.Post(testTransfer);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.Id, errors.Keys.First());
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
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
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
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfers = testTransferConverter.ToTransfers(testDomains.Value).ToList();
            var actionResult = await testController.Post(testTransfers);

            Assert.True(testDomainsIds.SequenceEqual(actionResult.Value));
        }

        /// <summary>
        /// Записать модели в базу. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task Post_Collection_ErrorDatabase()
        {
            var initialError = ErrorData.ErrorTest;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testService = DatabaseServicePostMock.GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverterMock.TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfers = testTransferConverter.ToTransfers(testDomains.Value).ToList();
            var actionResult = await testController.Post(testTransfers);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.Id, errors.Keys.First());
        }
    }
}