using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueMVC.Extensions.Controllers.Async;
using BoutiqueMVC.Extensions.Controllers.Sync;
using BoutiqueMVC.Models.Implementations.Controller;
using Functional.Models.Implementations.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using static BoutiqueMVCXUnit.Data.ErrorData;
using static BoutiqueMVCXUnit.Data.Collections;
using Functional.Models.Interfaces.Result;

namespace BoutiqueMVCXUnit.Extensions.Controllers.Async
{
    /// <summary>
    /// Преобразование результирующего ответа в ответ контроллера. Тесты
    /// </summary>
    public class ResultValueToActionResultAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера для задачи-объекта. Вернуть корректный объект
        /// </summary>
        [Fact]
        public async Task ToGetActionResultTaskAsync_OkRequest()
        {
            const int initialNumber = 2;
            var numberResult = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialNumber));

            var actionResult = await numberResult.ToGetActionResultTaskAsync();

            Assert.IsType<OkObjectResult>(actionResult);
            var okRequest = (OkObjectResult)actionResult;
            Assert.Equal(StatusCodes.Status200OK, okRequest.StatusCode);
            Assert.Equal(initialNumber, okRequest.Value);
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера для задачи-объекта. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public async Task ToGetActionResultTaskAsync_BadRequest()
        {
            var initialError = CreateErrorTest();
            var numberResult = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialError));

            var actionResult = await numberResult.ToGetActionResultTaskAsync();

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ в Json ответ для задачи-объекта. Вернуть корректный объект
        /// </summary>
        [Fact]
        public async Task ToGetJsonResultTaskAsync_OkRequest()
        {
            const int initialNumber = 2;
            var numberResult = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialNumber));

            var actionResult = await numberResult.ToGetJsonResultTaskAsync();

            Assert.IsType<JsonResult>(actionResult);
            var jsonRequest = (JsonResult)actionResult;
            Assert.Equal(StatusCodes.Status200OK, jsonRequest.StatusCode);
            Assert.Equal(initialNumber, jsonRequest.Value);
        }

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера для задачи-объекта. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public async Task ToGetJsonResultTaskAsync_BadRequest()
        {
            var initialError = CreateErrorTest();
            var numberResult = Task.FromResult((IResultValue<int>)new ResultValue<int>(initialError));

            var actionResult = await numberResult.ToGetJsonResultTaskAsync();

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ с коллекцией в Json ответ для задачи-объекта. Вернуть корректный объект
        /// </summary>
        [Fact]
        public async Task ToGetJsonResultCollectionTaskAsync_OkRequest()
        {
            var numbers = GetRangeNumber();
            var numbersResult = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(numbers));

            var actionResult = await numbersResult.ToGetJsonResultCollectionTaskAsync();

            Assert.IsType<JsonResult>(actionResult);
            var jsonRequest = (JsonResult)actionResult;
            Assert.Equal(StatusCodes.Status200OK, jsonRequest.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<int>>(jsonRequest.Value);
            Assert.True(numbers.SequenceEqual((IEnumerable<int>)jsonRequest.Value));
        }

        /// <summary>
        /// Преобразовать результирующий ответ с коллекцией в Json ответ для задачи-объекта. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public async Task ToGetJsonResultCollectionTaskAsync_BadRequest()
        {
            var initialError = CreateErrorTest();
            var numberResult = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(initialError));

            var actionResult = await numberResult.ToGetJsonResultCollectionTaskAsync();

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }

        /// <summary>
        /// Преобразовать результирующий ответ со значением в post ответ контроллера для задачи-объекта. Вернуть корректный ответ
        /// </summary>
        [Fact]
        public async Task ToPostActionResultTaskAsync_Created()
        {
            var ids = Enumerable.Range(1, 3).ToList();
            var values = ids.Select(number => number.ToString()).ToList();
            var idsResult = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(ids));
            var createdActionCollection = new CreatedActionCollection<string>("action", "controller", values);

            var actionResult = await idsResult.ToPostActionResultTaskAsync(createdActionCollection);

            Assert.IsType<CreatedAtActionResult>(actionResult);
            var createdAtActionResult = (CreatedAtActionResult)actionResult;
            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<string>>(createdAtActionResult.Value);
            Assert.True(values.SequenceEqual((IEnumerable<string>)createdAtActionResult.Value));
        }

        /// <summary>
        /// Преобразовать результирующий ответ со значением в post ответ контроллера для задачи-объекта. Вернуть объект с ошибкой
        /// </summary>
        [Fact]
        public async Task ToPostActionResultTaskAsync_BadRequest()
        {
            var ids = Enumerable.Range(1, 3).ToList();
            var values = ids.Select(number => number.ToString()).ToList();
            var initialError = CreateErrorTest();
            var idsResult = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(initialError));

            var createdActionCollection = new CreatedActionCollection<string>("action", "controller", values);

            var actionResult = await idsResult.ToPostActionResultTaskAsync(createdActionCollection);

            Assert.IsType<BadRequestObjectResult>(actionResult);
            var badRequest = (BadRequestObjectResult)actionResult;
            var errors = (SerializableError)badRequest.Value;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
            Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        }
    }
}