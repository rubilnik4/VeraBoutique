using System.Collections.Generic;
using BoutiqueDTO.Infrastructure.Implementation.Converters;
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
    public static class ResultValueToActionResultTaskAsyncExtensions
    {
        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера
        /// </summary>
        public static IActionResult ToGetActionResult<TValue>(this IResultValue<TValue> @this) =>
            @this.OkStatus
                ? (IActionResult)new OkObjectResult(@this.Value)
                : new BadRequestObjectResult(@this.Errors.ErrorsResultToModelState());

        /// <summary>
        /// Преобразовать результирующий ответ в Json ответ
        /// </summary>
        public static IActionResult ToGetJsonResult<TValue>(this IResultValue<TValue> @this) =>
            @this.
            ResultValueBindOk(value => ResultValueTry(() => new JsonResult(value) { StatusCode = StatusCodes.Status200OK},
                                                      GenderDtoConverter.ErrorJsonConverting(typeof(TValue).Name))).
            WhereContinue(resultJson => resultJson.OkStatus,
                okFunc: resultJson => (IActionResult)resultJson.Value,
                badFunc: resultJson => new BadRequestObjectResult(resultJson.Errors.ErrorsResultToModelState()));

        /// <summary>
        /// Преобразовать результирующий ответ с коллекцией в Json ответ
        /// </summary>
        public static IActionResult ToGetJsonResultCollection<TValue>(this IResultCollection<TValue> @this) =>
            @this.
            ResultValueBindOk(value => ResultValueTry(() => new JsonResult(value) { StatusCode = StatusCodes.Status200OK },
                                                            GenderDtoConverter.ErrorJsonConverting(typeof(TValue).Name))).
            WhereContinue(resultJson => resultJson.OkStatus,
                okFunc: resultJson => (IActionResult)resultJson.Value,
                badFunc: resultJson => new BadRequestObjectResult(resultJson.Errors.ErrorsResultToModelState()));

        ///// <summary>
        ///// Преобразовать результирующий ответ в ответ контроллера
        ///// </summary>
        //public static IActionResult ToPostActionResult<TValue>(this IResultError @this) =>
        //    @this.OkStatus
        //        ? (IActionResult)new Object(@this.Value)
        //        : new BadRequestObjectResult(ResultErrorsToModelState(@this.Errors));

      

    }
}