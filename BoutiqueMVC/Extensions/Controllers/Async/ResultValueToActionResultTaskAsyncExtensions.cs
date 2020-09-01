using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Implementation.Converters;
using BoutiqueMVC.Extensions.Controllers.Sync;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;
using Microsoft.AspNetCore.Mvc;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue.ResultValueTryExtensions;

namespace BoutiqueMVC.Extensions.Controllers.Async
{
    /// <summary>
    /// Преобразование результирующего ответа в ответ контроллера для задачи-объекта
    /// </summary>
    public static class ResultValueToActionResultTaskAsyncExtensions
    {
        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера для задачи-объекта
        /// </summary>
        public static async Task<IActionResult> ToGetActionResultTaskAsync<TValue>(this Task<IResultValue<TValue>> @this) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToGetActionResult());

        /// <summary>
        /// Преобразовать результирующий ответ в Json ответ для задачи-объекта
        /// </summary>
        public static async Task<IActionResult> ToGetJsonResultTaskAsync<TValue>(this Task<IResultValue<TValue>> @this) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToGetJsonResult());

        /// <summary>
        /// Преобразовать результирующий ответ с коллекцией в Json ответ для задачи-объекта
        /// </summary>
        public static async Task<IActionResult> ToGetJsonResultCollectionTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToGetJsonResultCollection());
    }
}