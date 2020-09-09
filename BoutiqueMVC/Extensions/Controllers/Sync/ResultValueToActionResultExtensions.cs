﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using BoutiqueDTO.Infrastructure.Implementations.Converters;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;
using BoutiqueMVC.Models.Implementations.Controller;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
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
        /// Преобразовать результирующий ответ в ответ контроллера со значением
        /// </summary>
        public static ActionResult<TTransfer> ToActionResultValue<TId, TTransfer>(this IResultValue<TTransfer> @this)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            @this.OkStatus
                ? new ActionResult<TTransfer>(@this.Value)
                : GetBadRequestByErrors<TTransfer>(@this.Errors);

        /// <summary>
        /// Преобразовать результирующий ответ с коллекцией в ответ контроллера со значением
        /// </summary>
        public static ActionResult<IReadOnlyCollection<TTransfer>> ToActionResultCollection<TId, TTransfer>(this IResultCollection<TTransfer> @this)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            @this.OkStatus
                ? new ActionResult<IReadOnlyCollection<TTransfer>>(@this.Value)
                : GetBadRequestByErrors<IReadOnlyCollection<TTransfer>>(@this.Errors);

        /// <summary>
        /// Преобразовать результирующий ответ со значением в ответ контроллера о создании объекта
        /// </summary>
        public static ActionResult<IReadOnlyCollection<TId>> ToCreateActionResult<TId, TTransfer>(this IResultCollection<TId> @this,
                                                                                                CreatedActionCollection<TTransfer> createdActionCollection)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            @this.OkStatus
                ? createdActionCollection.ToCreatedAtActionResult(@this.Value)
                : GetBadRequestByErrors<IReadOnlyCollection<TId>>(@this.Errors);

        /// <summary>
        /// Преобразовать результирующий ответ со значением в ответ контроллера о изменении объекта
        /// </summary>
        public static IActionResult ToNoContentActionResult(this IResultError @this) =>
            @this.OkStatus
                ? new NoContentResult()
                : GetBadRequestByErrors(@this.Errors);

        /// <summary>
        /// Преобразовать результирующий ответ в Json ответ
        /// </summary>
        public static IActionResult ToJsonResult<TId, TTransfer>(this IResultValue<TTransfer> @this)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            @this.
            ResultValueTryOk(value => new JsonResult(value) { StatusCode = StatusCodes.Status200OK },
                                 GenderTransferConverter.ErrorJsonConverting(typeof(TTransfer).Name)).
            WhereContinue(resultJson => resultJson.OkStatus,
                okFunc: resultJson => (IActionResult)resultJson.Value,
                badFunc: resultJson => GetBadRequestByErrors(resultJson.Errors));

        /// <summary>
        /// Преобразовать результирующий ответ с коллекцией в Json ответ
        /// </summary>
        public static IActionResult ToJsonResultCollection<TId, TTransfer>(this IResultCollection<TTransfer> @this)
            where TTransfer : ITransferModel<TId>
            where TId : notnull =>
            @this.
            ResultValueTryOk(value => new JsonResult(value) { StatusCode = StatusCodes.Status200OK },
                                     GenderTransferConverter.ErrorJsonConverting(typeof(TTransfer).Name)).
            WhereContinue(resultJson => resultJson.OkStatus,
                okFunc: resultJson => (IActionResult)resultJson.Value,
                badFunc: resultJson => GetBadRequestByErrors(resultJson.Errors));

        /// <summary>
        /// Получить объект со значением или вернуть ошибку
        /// </summary>
        private static ActionResult<TValue> GetBadRequestByErrors<TValue>(IReadOnlyCollection<IErrorResult> errors) =>
            GetBadRequestByErrors(errors);

        /// <summary>
        /// Получить объект со значением или вернуть ошибку
        /// </summary>
        private static ActionResult GetBadRequestByErrors(IReadOnlyCollection<IErrorResult> errors) =>
            errors.First().ErrorResultType switch
            {
                ErrorResultType.DatabaseValueNotFound => new NotFoundResult(),
                _ => new BadRequestObjectResult(errors.ToModelState()),
            };
    }
}