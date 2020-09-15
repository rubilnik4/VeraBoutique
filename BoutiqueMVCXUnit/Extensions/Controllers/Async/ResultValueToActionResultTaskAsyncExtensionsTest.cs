using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueMVC.Extensions.Controllers.Async;
using BoutiqueMVC.Extensions.Controllers.Sync;
using BoutiqueMVC.Models.Implementations.Controller;
using Functional.Models.Implementations.Result;
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
        ///// <summary>
        ///// Преобразовать результирующий ответ в ответ контроллера для задачи-объекта. Вернуть корректный объект
        ///// </summary>
        //[Fact]
        //public async Task ToActionResultTaskAsync_OkRequest()
        //{
        //    var initialTransfer = new Mock<ITransferModel<int>>();
        //    var numberResult = Task.FromResult((IResultValue<ITransferModel<int>>)new ResultValue<ITransferModel<int>>(initialTransfer.Object));

        //    var actionResult = await numberResult.ToActionResultValueTaskAsync<int, ITransferModel<int>>();

        //    Assert.IsType<OkObjectResult>(actionResult);
        //    var okRequest = (OkObjectResult)actionResult.Result;
        //    Assert.Equal(StatusCodes.Status200OK, okRequest.StatusCode);
        //    Assert.Equal(initialTransfer.Object, okRequest.Value);
        //}

        ///// <summary>
        ///// Преобразовать результирующий ответ в ответ контроллера для задачи-объекта. Вернуть объект с ошибкой
        ///// </summary>
        //[Fact]
        //public async Task ToActionResultTaskAsync_BadRequest()
        //{
        //    var initialError = CreateErrorTest();
        //    var numberResult = Task.FromResult((IResultValue<ITransferModel<int>>)new ResultValue<ITransferModel<int>>(initialError));

        //    var actionResult = await numberResult.ToActionResultValueTaskAsync<int, ITransferModel<int>>();

        //    Assert.IsType<BadRequestObjectResult>(actionResult);
        //    var badRequest = (BadRequestObjectResult)actionResult.Result;
        //    var errors = (SerializableError)badRequest.Value;
        //    Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        //    Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        //}

        ///// <summary>
        ///// Преобразовать результирующий ответ со значением в post ответ контроллера для задачи-объекта. Вернуть корректный ответ
        ///// </summary>
        //[Fact]
        //public async Task ToPostActionResultTaskAsync_Created()
        //{
        //    var ids = Enumerable.Range(1, 3).ToList();
        //    var initialTransfer = new Mock<IEnumerable<ITransferModel<int>>>();
        //    var idsResult = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(ids));
        //    var createdActionCollection = new CreatedActionCollection<ITransferModel<int>>("action", "controller", initialTransfer.Object);

        //    var actionResult = await idsResult.ToCreateActionResultTaskAsync(createdActionCollection);

        //    Assert.IsType<CreatedAtActionResult>(actionResult);
        //    var createdAtActionResult = (CreatedAtActionResult)actionResult.Result;
        //    Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
        //    Assert.IsAssignableFrom<IEnumerable<string>>(createdAtActionResult.Value);
        //    Assert.True(initialTransfer.Object.SequenceEqual((IEnumerable<ITransferModel<int>>)createdAtActionResult.Value));
        //}

        ///// <summary>
        ///// Преобразовать результирующий ответ со значением в post ответ контроллера для задачи-объекта. Вернуть объект с ошибкой
        ///// </summary>
        //[Fact]
        //public async Task ToPostActionResultTaskAsync_BadRequest()
        //{
        //    var initialTransfer = new Mock<IEnumerable<ITransferModel<int>>>();
        //    var initialError = CreateErrorTest();
        //    var idsResult = Task.FromResult((IResultCollection<int>)new ResultCollection<int>(initialError));

        //    var createdActionCollection = new CreatedActionCollection<ITransferModel<int>>("action", "controller", initialTransfer.Object);

        //    var actionResult = await idsResult.ToCreateActionResultTaskAsync(createdActionCollection);

        //    Assert.IsType<BadRequestObjectResult>(actionResult);
        //    var badRequest = (BadRequestObjectResult)actionResult.Result;
        //    var errors = (SerializableError)badRequest.Value;
        //    Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        //    Assert.Equal(initialError.ErrorResultType.ToString(), errors.Keys.First());
        //}
    }
}