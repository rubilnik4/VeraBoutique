using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Data.Services.Implementations;
using BoutiqueDTO.Data.Services.Interfaces;
using BoutiqueMVCXUnit.Data.Controllers.Implementations;
using Functional.Models.Implementations.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using static BoutiqueMVCXUnit.Controllers.Base.Mocks.TestDatabaseServiceMock;

namespace BoutiqueMVCXUnit.Controllers.Base
{
    /// <summary>
    /// Базовый контроллер для Api. Тесты
    /// </summary>
    public class BaseApiControllerTest
    {
        /// <summary>
        /// Получить модели из базы. Корректный вариант
        /// </summary>
        [Fact]
        public async Task Get_Ok()
        {
            var testDomains = TestData.TestResultDomains;
            var testService = GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var actionResult = await testController.Get();
            var testsAfter = testTransferConverter.FromTransfers(actionResult.Value);

            Assert.True(testsAfter.SequenceEqual(testDomains.Value));
        }

        /// <summary>
        /// Получить модели из базы. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task Get_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testService = GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverter;
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
            var testService = GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var actionResult = await testController.Get(testGetId);
            var testAfter = testTransferConverter.FromTransfer(actionResult.Value);

            Assert.True(testAfter.Equals(testGet));
        }

        /// <summary>
        /// Получить модель из базы по идентификатору. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task GetById_ErrorDatabase()
        {
            var testGetId = TestData.GetTestDomains().Last().Id;
            var initialError = ErrorData.DatabaseError;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testService = GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverter;
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
            var testGetId = TestData.GetTestDomains().Last().Id;
            var initialError = ErrorData.DatabaseError;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testService = GetTestDatabaseTableGet(testDomains, GetByIdNotFoundFunc());
            var testTransferConverter = TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var actionResult = await testController.Get(testGetId);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Записать модели в базу. Корректный вариант
        /// </summary>
        [Fact]
        public async Task Post_Ok()
        {
            var testDomains = TestData.TestResultDomains;
            var testDomainsIds = TestData.GetTestIds(testDomains.Value);
            var testService = GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverter;
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
        public async Task Post_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testService = GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfers = testTransferConverter.ToTransfers(testDomains.Value).ToList();
            var actionResult = await testController.Post(testTransfers);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Изменить модель в базе. Корректный вариант
        /// </summary>
        [Fact]
        public async Task Put_Ok()
        {
            var testDomains = TestData.TestResultDomains;
            var testPut = testDomains.Value.Last();
            var testPutId = testPut.Id;
            var testService = GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverter;
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
            var testPutId = testPut.Id;
            var testService = GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverter;
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
            var testPutId = testPut.Id;
            var testService = GetTestDatabaseTablePut(testDomains, PutNotFoundFunc());
            var testTransferConverter = TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var testTransfer = testTransferConverter.ToTransfer(testPut);
            var actionResult = await testController.Put(testTransfer);

            Assert.IsType<NotFoundResult>(actionResult);
            var notFoundResult = (NotFoundResult)actionResult;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Изменить модель в базе. Корректный вариант
        /// </summary>
        [Fact]
        public async Task Delete_Ok()
        {
            var testDomains = TestData.TestResultDomains;
            var testDelete = testDomains.Value.Last();
            var testDeleteId = testDelete.Id;
            var testService = GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var actionResult = await testController.Delete(testDeleteId);

            Assert.True(actionResult.Value.Equals(testDelete));
        }

        /// <summary>
        /// Изменить модель в базе. Ошибка базы данных
        /// </summary>
        [Fact]
        public async Task Delete_ErrorDatabase()
        {
            var initialError = ErrorData.DatabaseError;
            var testDomains = new ResultCollection<ITestDomain>(initialError);
            var testDelete = TestData.GetTestDomains().Last();
            var testDeleteId = testDelete.Id;
            var testService = GetTestDatabaseTable(testDomains);
            var testTransferConverter = TestTransferConverter;
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
            var testService = GetTestDatabaseTableDelete(testDomains, DeleteNotFoundFunc());
            var testTransferConverter = TestTransferConverter;
            var testController = new TestController(testService.Object, testTransferConverter);

            var actionResult = await testController.Delete(testDeleteId);

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundResult = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        /// <summary>
        /// Конвертер в трансферную модель
        /// </summary>
        private static ITestTransferConverter TestTransferConverter => new TestTransferConverter();
    }
}