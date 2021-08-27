using System.Collections.Generic;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BoutiqueMVC.Extensions.Controllers.Sync
{
    /// <summary>
    /// Методы расширения для ошибок результирующего результата
    /// </summary>
    public static class ErrorResultToModelStateExtensions
    {
        /// <summary>
        /// Сформировать модель ошибок
        /// </summary>
        public static ModelStateDictionary ToModelState(this IEnumerable<IErrorResult> errors)
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in errors)
            {
                modelState.TryAddModelError(error.Id, error.Description);
            }
            return modelState;
        }
    }
}