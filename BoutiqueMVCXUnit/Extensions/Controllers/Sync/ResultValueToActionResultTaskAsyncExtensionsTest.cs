using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Models.Interfaces;
using BoutiqueMVC.Extensions.Controllers.Sync;
using BoutiqueMVC.Models.Implementations.Controller;
using BoutiqueMVCXUnit.Data;
using BoutiqueMVCXUnit.Properties;
using Functional.Models.Implementations.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BoutiqueMVCXUnit.Extensions.Controllers.Sync
{
    /// <summary>
    /// Преобразование результирующего ответа в ответ контроллера. Тесты
    /// </summary>
    public class ResultValueToActionResultExtensionsTest
    {
        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть корректный объект
        /// </summary>
        [Fact]
        public void ToActionResultValue_Id_OkRequest()
        {
            var id = TransferData.GetTestTransfer().Id;
            var testTransfer = new ResultValue<TestEnum>(id);

            var actionResult = testTransfer.ToActionResultValue();

            Assert.Equal(id, actionResult.Value);
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public void ToActionResultValue_Id_BadRequest()
        {
            var initialError = ErrorData.ErrorTest;
            var testTransfer = new ResultValue<TestEnum>(initialError);

            var actionResult = testTransfer.ToActionResultValue();

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть объект с ошибкой не найденного элемента
        /// </summary>
        [Fact]
        public void ToActionResultValue_Id_NotFound()
        {
            var initialError = ErrorData.NotFoundError;
            var testTransfer = new ResultValue<TestEnum>(initialError);

            var actionResult = testTransfer.ToActionResultValue();

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundRequest = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundRequest.StatusCode);
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть корректный объект
        /// </summary>
        [Fact]
        public void ToImageResultValue_Id_OkRequest()
        {
            var image = Resources.TestImage;
            var imageResult = new ResultValue<byte[]>(image);

            var actionResult = imageResult.ToImageResultValue();

            Assert.IsType<FileContentResult>(actionResult);
            var fileContentResult = (FileContentResult)actionResult;
            Assert.True(image.SequenceEqual(fileContentResult.FileContents));
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public void ToImageResultValue_Id_BadRequest()
        {
            var initialError = ErrorData.ErrorTest;
            var imageResult = new ResultValue<byte[]>(initialError);

            var actionResult = imageResult.ToImageResultValue();

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть объект с ошибкой не найденного элемента
        /// </summary>
        [Fact]
        public void ToImageResultValue_Id_NotFound()
        {
            var initialError = ErrorData.NotFoundError;
            var testTransfer = new ResultValue<byte[]>(initialError);

            var actionResult = testTransfer.ToImageResultValue();

            Assert.IsType<NotFoundResult>(actionResult);
            var notFoundRequest = (NotFoundResult)actionResult;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundRequest.StatusCode);
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть корректный объект
        /// </summary>
        [Fact]
        public void ToActionResultValue_OkRequest()
        {
            var initialTransfer = TransferData.GetTestTransfer();
            var testTransfer = new ResultValue<ITestTransfer>(initialTransfer);

            var actionResult = testTransfer.ToActionResultValue<TestEnum, ITestTransfer>();

            Assert.Equal(initialTransfer, actionResult.Value);
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public void ToActionResultValue_BadRequest()
        {
            var initialError = ErrorData.ErrorTest;
            var testTransfer = new ResultValue<ITestTransfer>(initialError);

            var actionResult = testTransfer.ToActionResultValue<TestEnum, ITestTransfer>();

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть объект с ошибкой не найденного элемента
        /// </summary>
        [Fact]
        public void ToActionResultValue_NotFound()
        {
            var initialError = ErrorData.NotFoundError;
            var testTransfer = new ResultValue<ITestTransfer>(initialError);

            var actionResult = testTransfer.ToActionResultValue<TestEnum, ITestTransfer>();

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundRequest = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundRequest.StatusCode);
        }

        /// <summary>
        /// Преобразовать результирующий ответ коллекцию в ответ контроллера. Вернуть корректный объект
        /// </summary>
        [Fact]
        public void ToActionResultCollection_OkRequest()
        {
            var initialTransfer = TransferData.GetTestTransfers();
            var testTransfer = new ResultCollection<ITestTransfer>(initialTransfer);

            var actionResult = testTransfer.ToActionResultCollection<TestEnum, ITestTransfer>();

            Assert.True(initialTransfer.SequenceEqual(actionResult.Value));
        }


        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public void ToActionResultCollection_BadRequest()
        {
            var initialError = ErrorData.ErrorTest;
            var testTransfer = new ResultCollection<ITestTransfer>(initialError);

            var actionResult = testTransfer.ToActionResultCollection<TestEnum, ITestTransfer>();

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть объект с ошибкой не найденного элемента
        /// </summary>
        [Fact]
        public void ToActionResultCollection_NotFound()
        {
            var initialError = ErrorData.NotFoundError;
            var testTransfer = new ResultCollection<ITestTransfer>(initialError);

            var actionResult = testTransfer.ToActionResultCollection<TestEnum, ITestTransfer>();

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundRequest = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundRequest.StatusCode);
        }

        /// <summary>
        /// Преобразовать результирующий ответ коллекцию в ответ контроллера. Вернуть корректный объект
        /// </summary>
        [Fact]
        public void ToActionResultCollection_Id_OkRequest()
        {
            var ids = TransferData.GetTestTransfers().Select(test => test.Id).ToList();
            var testTransfer = new ResultCollection<TestEnum>(ids);

            var actionResult = testTransfer.ToActionResultCollection();

            Assert.True(ids.SequenceEqual(actionResult.Value));
        }


        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public void ToActionResultCollection_Id_BadRequest()
        {
            var initialError = ErrorData.ErrorTest;
            var testTransfer = new ResultCollection<ITestTransfer>(initialError);

            var actionResult = testTransfer.ToActionResultCollection();

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть объект с ошибкой не найденного элемента
        /// </summary>
        [Fact]
        public void ToActionResultCollection_Id_NotFound()
        {
            var initialError = ErrorData.NotFoundError;
            var testTransfer = new ResultCollection<ITestTransfer>(initialError);

            var actionResult = testTransfer.ToActionResultCollection();

            Assert.IsType<NotFoundResult>(actionResult.Result);
            var notFoundRequest = (NotFoundResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status404NotFound, notFoundRequest.StatusCode);
        }

        /// <summary>
        /// Преобразовать результирующий ответ со значением в post ответ контроллера. Вернуть корректный ответ
        /// </summary>
        [Fact]
        public void ToPostActionResultValue_Created()
        {
            var createdActionValue = CreateActionData.CreatedActionValue;
            var createdResult = new ResultValue<CreatedActionValue<TestEnum, ITestTransfer>>(createdActionValue);

            var actionResult = createdResult.ToCreateActionResult();
            var createdAtActionResult = (CreatedAtActionResult)actionResult.Result;

            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
            Assert.True(createdActionValue.Value.Equals((ITestTransfer)createdAtActionResult.Value));
            Assert.True(createdActionValue.Id.Equals((TestEnum)createdAtActionResult.RouteValues.Values.First()!));
        }

        /// <summary>
        /// Преобразовать результирующий ответ со значением в post ответ контроллера. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public void ToPostActionResultValue_BadRequest()
        {
            var initialError = ErrorData.ErrorTest;
            var createdResult = new ResultValue<CreatedActionValue<TestEnum, ITestTransfer>>(initialError);

            var actionResult = createdResult.ToCreateActionResult();
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ со значением в post ответ контроллера. Вернуть корректный ответ
        /// </summary>
        [Fact]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void ToPostActionResultCollection_Created()
        {
            var createdActionCollection = CreateActionData.CreatedActionCollection;
            var createdResult = new ResultValue<CreatedActionCollection<TestEnum, ITestTransfer>>(createdActionCollection);

            var actionResult = createdResult.ToCreateActionResult();
            var createdAtActionResult = (CreatedAtActionResult)actionResult.Result;

            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
            Assert.True(createdActionCollection.Values.SequenceEqual((IEnumerable<ITestTransfer>)createdAtActionResult.Value));
            Assert.True(createdActionCollection.Ids.SequenceEqual((IEnumerable<TestEnum>)createdAtActionResult.RouteValues.Values.First()!));
        }

        /// <summary>
        /// Преобразовать результирующий ответ со значением в post ответ контроллера. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public void ToPostActionResultCollection_BadRequest()
        {
            var initialError = ErrorData.ErrorTest;
            var createdResult = new ResultValue<CreatedActionCollection<TestEnum, ITestTransfer>>(initialError);

            var actionResult = createdResult.ToCreateActionResult();
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;

            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера без значения. Вернуть корректный объект
        /// </summary>
        [Fact]
        public void ToNoContentActionResult_OkRequest()
        {
            var result = new ResultError();

            var actionResult = result.ToNoContentActionResult();

            Assert.IsType<NoContentResult>(actionResult);
            var noContentResult = (NoContentResult)actionResult;
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера без значения. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public void ToNoContentActionResult_BadRequest()
        {
            var initialError = ErrorData.ErrorTest;
            var result = new ResultError(initialError);

            var actionResult = result.ToNoContentActionResult();

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }
    }
}