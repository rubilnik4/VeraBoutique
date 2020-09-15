using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueMVC.Extensions.Controllers.Sync;
using BoutiqueMVC.Models.Implementations.Controller;
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
        /// Преобразовать результирующий ответ в ответ контроллера со значением для задачи-объекта
        /// </summary>
        public static async Task<ActionResult<TTransfer>> ToActionResultValueTaskAsync<TId, TTransfer>(this Task<IResultValue<TTransfer>> @this)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToActionResultValue<TId, TTransfer>());

        /// <summary>
        /// Преобразовать результирующий ответ с коллекцией в ответ контроллера со значением для задачи-объекта
        /// </summary>
        public static async Task<ActionResult<IReadOnlyCollection<TTransfer>>> ToActionResultCollectionTaskAsync<TId, TTransfer>(this Task<IResultCollection<TTransfer>> @this)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToActionResultCollection<TId, TTransfer>());

        /// <summary>
        /// Преобразовать результирующий ответ со значением в ответ контроллера о создании объекта асинхронно
        /// </summary>
        public static async Task<ActionResult<IReadOnlyCollection<TId>>> ToCreateActionResultTaskAsync<TId, TTransfer>(this Task<IResultCollection<TId>> @this,
                                                                                                                     CreatedActionCollection<TTransfer> createdActionCollection)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToCreateActionResult(createdActionCollection));

        /// <summary>
        /// Преобразовать результирующий ответ со значением в ответ контроллера о изменении объекта асинхронно
        /// </summary>
        public static async Task<IActionResult> ToNoContentActionResultTaskAsync(this Task<IResultError> @this) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToNoContentActionResult());
    }
}