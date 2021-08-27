﻿using System;
using System.Threading.Tasks;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Errors;
using Functional.Models.Interfaces.Errors.Base;
using Functional.Models.Interfaces.Results;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues.ResultValueTryExtensions;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием со значением и обработкой исключений
    /// </summary>
    public static class ResultValueBindTryExtensions
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со связыванием со значением или ошибку исключения
        /// </summary>
        public static IResultValue<TValue> ResultValueBindTry<TValue>(Func<IResultValue<TValue>> func, IErrorResult error)
        {
            IResultValue<TValue> funcResult;

            try
            {
                funcResult = func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultValue<TValue>(error.AppendException(ex));
            }

            return funcResult;
        }

        /// <summary>
        /// Связать результирующий ответ со значением со связыванием с обработкой функции при положительном условии
        /// </summary>
        public static IResultValue<TValueOut> ResultValueBindTryOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, IResultValue<TValueOut>> func,
                                                                                    IErrorResult error) =>
            @this.ResultValueBindOk(value => ResultValueBindTry(() => func.Invoke(value), error));
    }
}