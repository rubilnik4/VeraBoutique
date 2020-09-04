using System;
using System.Collections.Generic;
using BoutiqueDTO.Infrastructure.Implementation.Converters;
using BoutiqueMVC.Models.Implementations.Controller;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue.ResultValueTryExtensions;

namespace BoutiqueMVC.Extensions.Controllers.Sync
{
    /// <summary>
    /// Преобразование результирующего ответа в ответ контроллера
    /// </summary>
    public static class ResultValueToActionResultExtensions
    {
        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера
        /// </summary>
        public static IActionResult ToGetActionResult<TValue>(this IResultValue<TValue> @this) =>
            @this.OkStatus
                ? (IActionResult)new OkObjectResult(@this.Value)
                : new BadRequestObjectResult(@this.Errors.ToModelState());

        /// <summary>
        /// Преобразовать результирующий ответ в Json ответ
        /// </summary>
        public static IActionResult ToGetJsonResult<TValue>(this IResultValue<TValue> @this) =>
            @this.
            ResultValueBindTryOk(value => new JsonResult(value) { StatusCode = StatusCodes.Status200OK },
                                 GenderDtoConverter.ErrorJsonConverting(typeof(TValue).Name)).
            WhereContinue(resultJson => resultJson.OkStatus,
                okFunc: resultJson => (IActionResult)resultJson.Value,
                badFunc: resultJson => new BadRequestObjectResult(resultJson.Errors.ToModelState()));

        /// <summary>
        /// Преобразовать результирующий ответ с коллекцией в Json ответ
        /// </summary>
        public static IActionResult ToGetJsonResultCollection<TValue>(this IResultCollection<TValue> @this) =>
            @this.
            ResultValueBindTryOk(value => new JsonResult(value) { StatusCode = StatusCodes.Status200OK },
                                     GenderDtoConverter.ErrorJsonConverting(typeof(TValue).Name)).
            WhereContinue(resultJson => resultJson.OkStatus,
                okFunc: resultJson => (IActionResult)resultJson.Value,
                badFunc: resultJson => new BadRequestObjectResult(resultJson.Errors.ToModelState()));

        /// <summary>
        /// Преобразовать результирующий ответ со значением в post ответ контроллера
        /// </summary>
        public static IActionResult ToPostActionResult<TId, TValue>(this IResultCollection<TId> @this,
                                                                    CreatedActionCollection<TValue> createdActionCollection) =>
            @this.
            WhereContinue(result => result.OkStatus,
                okFunc: result => (IActionResult)new CreatedAtActionResult(createdActionCollection.ActionGetName, createdActionCollection.ControllerName,
                                                                           new { ids = @this.Value }, createdActionCollection.Values),
                badFunc: result => new BadRequestObjectResult(result.Errors.ToModelState()));
    }
}