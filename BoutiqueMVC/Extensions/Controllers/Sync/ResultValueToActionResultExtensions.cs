﻿using System;
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
        /// Преобразовать результирующий ответ в Json ответ
        /// </summary>
        public static IActionResult ToPostActionResult(this IResultError @this) =>
            @this.
            WhereContinue(result => result.OkStatus,
                okFunc: result => (IActionResult)new CreatedAtActionResult(nameof(GetById), ,new { id = product.Id }, product),
                badFunc: result => new BadRequestObjectResult(result.Errors.ToModelState()));



    }
}