using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Models.Interfaces;
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
            var initialError = ErrorData.ErrorTypeTest;
            var testTransfer = ResultValueFactory.CreateTaskResultValueError<ITestTransfer>(initialError);

            var actionResult = await testTransfer.ToActionResultValueTaskAsync<TestEnum, ITestTransfer>();

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера асинхронно. Вернуть объект с ошибкой не найденного элемента
        /// </summary>
        [Fact]
        public async Task ToActionResultValueAsync_NotFound()
        {
            var initialError = ErrorData.NotFoundErrorType;
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
            var initialError = ErrorData.ErrorTypeTest;
            var testTransfer = ResultCollectionFactory.CreateTaskResultCollectionError<ITestTransfer>(initialError);

            var actionResult = await testTransfer.ToActionResultCollectionTaskAsync<TestEnum, ITestTransfer>();

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера асинхронно. Вернуть объект с ошибкой не найденного элемента
        /// </summary>
        [Fact]
        public async Task ToActionResultCollectionAsync_NotFound()
        {
            var initialError = ErrorData.NotFoundErrorType;
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
        public async Task ToPostActionResultValueAsync_Created()
        {
            var createdActionValue = CreateActionData.CreatedActionValue;
            var createdResult = ResultValueFactory.CreateTaskResultValue(createdActionValue);

            var actionResult = await createdResult.ToCreateActionResultTaskAsync();
            var createdAtActionResult = (CreatedAtActionResult)actionResult.Result;

            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
            Assert.True(createdActionValue.Value.Equals((ITestTransfer)createdAtActionResult.Value));
            Assert.True(createdActionValue.Id.Equals((TestEnum)createdAtActionResult.RouteValues.Values.First()));
        }

        /// <summary>
        /// Преобразовать результирующий ответ со значением в post ответ контроллера асинхронно. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public async Task ToPostActionResultValueAsync_BadRequest()
        {
            var initialError = ErrorData.ErrorTypeTest;
            var createdResult = ResultValueFactory.CreateTaskResultValueError<CreatedActionValue<TestEnum, ITestTransfer>>(initialError);

            var actionResult = await createdResult.ToCreateActionResultTaskAsync();
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ со значением в post ответ контроллера асинхронно. Вернуть корректный ответ
        /// </summary>
        [Fact]
        public async Task ToPostActionResultCollectionAsync_Created()
        {
            var createdActionCollection = CreateActionData.CreatedActionCollection;
            var createdResult = ResultValueFactory.CreateTaskResultValue(createdActionCollection);

            var actionResult = await createdResult.ToCreateActionResultTaskAsync();
            var createdAtActionResult = (CreatedAtActionResult)actionResult.Result;

            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
            Assert.True(createdActionCollection.Values.SequenceEqual((IEnumerable<ITestTransfer>)createdAtActionResult.Value));
            Assert.True(createdActionCollection.Ids.SequenceEqual((IEnumerable<TestEnum>)createdAtActionResult.RouteValues.Values.First()));
        }

        /// <summary>
        /// Преобразовать результирующий ответ со значением в post ответ контроллера асинхронно. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public async Task ToPostActionResultCollectionAsync_BadRequest()
        {
            var initialError = ErrorData.ErrorTypeTest;
            var createdResult = ResultValueFactory.CreateTaskResultValueError<CreatedActionCollection<TestEnum, ITestTransfer>>(initialError);

            var actionResult = await createdResult.ToCreateActionResultTaskAsync();
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorType.ToString(), errors.Keys.First());
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
            var initialError = ErrorData.ErrorTypeTest;
            var result = ResultErrorFactory.CreateTaskResultError(initialError);

            var actionResult = await result.ToNoContentActionResultTaskAsync();

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorType.ToString(), errors.Keys.First());
        }
    }
}