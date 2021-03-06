﻿using System;
using Functional.Models.Interfaces.Result;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений
    /// </summary>
    public static class ResultValueTryExtensions
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static IResultValue<TValue> ResultValueTry<TValue>(Func<TValue> func, IErrorResult error)
        {
            TValue funcResult;

            try
            {
                funcResult = func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultValue<TValue>(error.AppendException(ex));
            }

            return new ResultValue<TValue>(funcResult);
        }

        /// <summary>
        /// Результирующий ответ со значением с обработкой функции при положительном условии
        /// </summary>
        public static IResultValue<TValueOut> ResultValueTryOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, TValueOut> func, IErrorResult error) =>
            @this.ResultValueBindOk(value => ResultValueTry(() => func.Invoke(value), error));
    }
}