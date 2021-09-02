using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueMVC.Extensions.Controllers.Sync;
using BoutiqueMVC.Models.Implementations.Controller;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;
using Microsoft.AspNetCore.Mvc;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues.ResultValueTryExtensions;

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
        public static async Task<ActionResult<TId>> ToActionResultValueTaskAsync<TId>(this Task<IResultValue<TId>> @this)
            where TId : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToActionResultValue());

        /// <summary>
        /// Преобразовать результирующий ответ в ответ контроллера со значением для задачи-объекта
        /// </summary>
        public static async Task<ActionResult> ToImageResultValueTaskAsync(this Task<IResultValue<byte[]>> @this) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToImageResultValue());

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
        public static async Task<ActionResult<IReadOnlyCollection<TId>>> ToActionResultCollectionTaskAsync<TId>(this Task<IResultCollection<TId>> @this)
            where TId : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToActionResultCollection());

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
        public static async Task<ActionResult<TId>> ToCreateActionResultTaskAsync<TId, TTransfer>(this Task<IResultValue<CreatedActionValue<TId, TTransfer>>> @this)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToCreateActionResult());

        /// <summary>
        /// Преобразовать результирующий ответ со значением в ответ контроллера о создании объекта асинхронно
        /// </summary>
        public static async Task<ActionResult<IReadOnlyCollection<TId>>> ToCreateActionResultTaskAsync<TId, TTransfer>(this Task<IResultValue<CreatedActionCollection<TId, TTransfer>>> @this)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToCreateActionResult());

        /// <summary>
        /// Преобразовать результирующий ответ со значением в ответ контроллера о изменении объекта асинхронно
        /// </summary>
        public static async Task<IActionResult> ToNoContentActionResultTaskAsync(this Task<IResultError> @this) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToNoContentActionResult());
    }
}