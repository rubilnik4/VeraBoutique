using System.Linq;
using BoutiqueMVC.Extensions.Controllers.Sync;
using Functional.Models.Implementations.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using static BoutiqueMVCXUnit.Data.ErrorData;
using static BoutiqueMVCXUnit.Data.Collections;
using System.Collections;
using System.Collections.Generic;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueMVC.Models.Implementations.Controller;
using Moq;

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
        public void ToGetActionResult_OkRequest()
        {
            var initialTransfer = new Mock<ITransferModel<int>>();
            var numberResult = new ResultValue<ITransferModel<int>>(initialTransfer.Object);

            var actionResult = numberResult.ToActionResultValue<int, ITransferModel<int>>();

            Assert.IsType<OkObjectResult>(actionResult);
            var okRequest = (OkObjectResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status200OK, okRequest.StatusCode);
            Assert.Equal(initialTransfer.Object, okRequest.Value);
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public void ToGetActionResult_BadRequest()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<ITransferModel<int>>(initialError);

            var actionResult = numberResult.ToActionResultValue<int, ITransferModel<int>>();

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ в Json ответ. Вернуть корректный объект
        /// </summary>
        [Fact]
        public void ToGetJsonResult_OkRequest()
        {
            var initialTransfer = new Mock<ITransferModel<int>>();
            var numberResult = new ResultValue<ITransferModel<int>>(initialTransfer.Object);

            var actionResult = numberResult.ToJsonResult<int, ITransferModel<int>>();

            Assert.IsType<JsonResult>(actionResult);
            var jsonRequest = (JsonResult)actionResult;
            Assert.Equal(StatusCodes.Status200OK, jsonRequest.StatusCode);
            Assert.Equal(initialTransfer.Object, jsonRequest.Value);
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public void ToGetJsonResult_BadRequest()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<ITransferModel<int>>(initialError);

            var actionResult = numberResult.ToJsonResult<int, ITransferModel<int>>();

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ с коллекцией в Json ответ. Вернуть корректный объект
        /// </summary>
        [Fact]
        public void ToGetJsonResultCollection_OkRequest()
        {
            var initialTransfer = new Mock<IEnumerable<ITransferModel<int>>>();
            var numbersResult = new ResultCollection<ITransferModel<int>>(initialTransfer.Object);

            var actionResult = numbersResult.ToJsonResultCollection<int, ITransferModel<int>>();

            Assert.IsType<JsonResult>(actionResult);
            var jsonRequest = (JsonResult)actionResult;
            Assert.Equal(StatusCodes.Status200OK, jsonRequest.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<int>>(jsonRequest.Value);
            Assert.True(initialTransfer.Object.SequenceEqual((IEnumerable<ITransferModel<int>>)jsonRequest.Value));
        }

        /// <summary>
        /// Преобразовать результирующий ответ с коллекцией в Json ответ. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public void ToGetJsonResultCollection_BadRequest()
        {
            var initialError = CreateErrorTest();
            var numbersResult = new ResultCollection<ITransferModel<int>>(initialError);

            var actionResult = numbersResult.ToJsonResultCollection<int, ITransferModel<int>>();

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ со значением в post ответ контроллера. Вернуть корректный ответ
        /// </summary>
        [Fact]
        public void ToPostActionResult_Created()
        {
            var ids = Enumerable.Range(1, 3).ToList();
            var initialTransfer = new Mock<IEnumerable<ITransferModel<int>>>();
            var idsResult = new ResultCollection<int>(ids);
            var createdActionCollection = new CreatedActionCollection<ITransferModel<int>>("action", "controller", initialTransfer.Object);

            var actionResult = idsResult.ToCreateActionResult(createdActionCollection);

            Assert.IsType<CreatedAtActionResult>(actionResult);
            var createdAtActionResult = (CreatedAtActionResult)actionResult.Result;
            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<string>>(createdAtActionResult.Value);
            Assert.True(initialTransfer.Object.SequenceEqual((IEnumerable<ITransferModel<int>>)createdAtActionResult.Value));
            Assert.IsAssignableFrom<IEnumerable<int>>(createdAtActionResult.RouteValues.Values.First());
            Assert.True(ids.SequenceEqual((IEnumerable<int>)createdAtActionResult.RouteValues.Values.First()));
        }

        /// <summary>
        /// Преобразовать результирующий ответ со значением в post ответ контроллера. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public void ToPostActionResult_BadRequest()
        {
            var initialTransfer = new Mock<IEnumerable<ITransferModel<int>>>();
            var initialError = CreateErrorTest();
            var idsResult = new ResultCollection<int>(initialError);

            var createdActionCollection = new CreatedActionCollection<ITransferModel<int>>("action", "controller", initialTransfer.Object);

            var actionResult = idsResult.ToCreateActionResult(createdActionCollection);

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult.Result;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }
    }
}