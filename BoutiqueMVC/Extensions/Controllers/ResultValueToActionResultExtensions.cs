using System.Collections;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Interfaces.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace BoutiqueMVC.Extensions.Controllers
{
    /// <summary>
    /// Преобразование результирующего ответа в ответ контроллера
    /// </summary>
    public static class ResultValueToActionResultExtensions
    {
        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера
        /// </summary>
        public static IActionResult ToActionResult<TValue>(this IResultValue<TValue> @this) =>
            @this.OkStatus
                ? (IActionResult) new OkObjectResult(@this.Value)
                : new BadRequestObjectResult(ResultErrorsToModelState(@this.Errors));

        /// <summary>
        /// Сформировать модель ошибок
        /// </summary>
        public static ModelStateDictionary ResultErrorsToModelState(IEnumerable<IErrorResult> errors)
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in errors)
            {
                modelState.TryAddModelError(error.ErrorResultType.ToString(), error.Description);
            }
            return modelState;
        }
            
    }
}