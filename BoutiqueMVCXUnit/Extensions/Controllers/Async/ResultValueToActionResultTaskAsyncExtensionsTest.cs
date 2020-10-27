using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTO.Data.Models.Interfaces;
using BoutiqueMVC.Extensions.Controllers.Async;
using BoutiqueMVC.Models.Implementations.Controller;
using BoutiqueMVCXUnit.Data;
using Functional.Models.Implementations.ResultFactory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BoutiqueMVCXUnit.Extensions.Controllers.Async
{
    /// <summary>
    /// Преобразование результирующего ответа в ответ контроллера. Тесты
    /// </summary>
    public class ResultValueToActionResultAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера асинхронно. Вернуть корректный объект
        /// </summary>
        [Fact]
        public async Task ToActionResultValueAsync_OkRequest()
        {
            var initialTransfer = TransferData.GetTestTransfer();
            var testTransfer = ResultValueFactory.CreateTaskResultValue(initialTransfer);

            var actionResult = await testTransfer.ToActionResultValueTaskAsync<TestEnum, ITestTransfer>();

            Assert.Equal(initialTransfer, actionResult.Value);
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера асинхронно. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public async Task ToActionResultValueAsync_BadRequest()
        {
            var initialError = ErrorData.ErrorTest;
            var testTransfer = ResultValueFactory.CreateTaskResultValueError<ITestTransfer>(initialError);

            var actionResult = await testTransfer.ToActionResultValueTaskAsync<TestEnum, ITestTransfer>();

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера асинхронно. Вернуть объект с ошибкой не найденного элемента
        /// </summary>
        [Fact]
        public async Task ToActionResultValueAsync_NotFound()
        {
            var initialError = ErrorData.NotFoundError;
            var testTransfer = ResultValueFactory.CreateTaskResultValueError<ITestTransfer>(initialError);

            var actionResult = await testTransfer.ToActionResultValueTaskAsync<TestEnum, ITestTransfer>();

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundRequest = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundRequest.StatusCode);
        }

        /// <summary>
        /// Преобразовать результирующий ответ коллекцию в ответ контроллера асинхронно. Вернуть корректный объект
        /// </summary>
        [Fact]
        public async Task ToActionResultCollectionAsync_OkRequest()
        {
            var initialTransfer = TransferData.GetTestTransfers();
            var testTransfer = ResultCollectionFactory.CreateTaskResultCollection(initialTransfer);

            var actionResult = await testTransfer.ToActionResultCollectionTaskAsync<TestEnum, ITestTransfer>();

            Assert.True(initialTransfer.SequenceEqual(actionResult.Value));
        }


        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера асинхронно. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public async Task ToActionResultCollectionAsync_BadRequest()
        {
            var initialError = ErrorData.ErrorTest;
            var testTransfer = ResultCollectionFactory.CreateTaskResultCollectionError<ITestTransfer>(initialError);

            var actionResult = await testTransfer.ToActionResultCollectionTaskAsync<TestEnum, ITestTransfer>();

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера асинхронно. Вернуть объект с ошибкой не найденного элемента
        /// </summary>
        [Fact]
        public async Task ToActionResultCollectionAsync_NotFound()
        {
            var initialError = ErrorData.NotFoundError;
            var testTransfer = ResultCollectionFactory.CreateTaskResultCollectionError<ITestTransfer>(initialError);

            var actionResult = await testTransfer.ToActionResultCollectionTaskAsync<TestEnum, ITestTransfer>();

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundRequest = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundRequest.StatusCode);
        }

        /// <summary>
        /// Преобразовать результирующий ответ со значением в post ответ контроллера асинхронно. Вернуть корректный ответ
        /// </summary>
        [Fact]
        public async Task ToPostActionResultAsync_Created()
        {
            var initialTransfer = TransferData.GetTestTransfers();
            var ids = TransferData.GetTestIds(initialTransfer);
            var idsResult = ResultCollectionFactory.CreateTaskResultCollection(ids);
            var createdActionCollection = new CreatedActionCollection<ITestTransfer>("action", "controller", initialTransfer);

            var actionResult = await idsResult.ToCreateActionResultTaskAsync(createdActionCollection);

            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var createdAtActionResult = (CreatedAtActionResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<ITestTransfer>>(createdAtActionResult.Value);
            Assert.True(initialTransfer.SequenceEqual((IEnumerable<ITestTransfer>)createdAtActionResult.Value));
            Assert.IsAssignableFrom<IEnumerable<TestEnum>>(createdAtActionResult.RouteValues.Values.First());
            Assert.True(ids.SequenceEqual((IEnumerable<TestEnum>)createdAtActionResult.RouteValues.Values.First()));
        }

        /// <summary>
        /// Преобразовать результирующий ответ со значением в post ответ контроллера асинхронно. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public async Task ToPostActionResultAsync_BadRequest()
        {
            var initialTransfer = TransferData.GetTestTransfers();
            var initialError = ErrorData.ErrorTest;
            var idsResult = ResultCollectionFactory.CreateTaskResultCollectionError<TestEnum>(initialError);
            var createdActionCollection = new CreatedActionCollection<ITestTransfer>("action", "controller", initialTransfer);

            var actionResult = await idsResult.ToCreateActionResultTaskAsync(createdActionCollection);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера без значения асинхронно. Вернуть корректный объект
        /// </summary>
        [Fact]
        public async Task ToNoContentActionResultAsync_OkRequest()
        {
            var result = ResultErrorFactory.CreateTaskResultError();

            var actionResult = await result.ToNoContentActionResultTaskAsync();

            Assert.IsType<NoContentResult>(actionResult);
            var noContentResult = (NoContentResult)actionResult;
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера без значения асинхронно. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public async Task ToNoContentActionResultAsync_BadRequest()
        {
            var initialError = ErrorData.ErrorTest;
            var result = ResultErrorFactory.CreateTaskResultError(initialError);

            var actionResult = await result.ToNoContentActionResultTaskAsync();

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }
    }
}