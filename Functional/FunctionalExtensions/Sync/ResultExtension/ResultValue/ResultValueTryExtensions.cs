using System;
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
    public static class ResultErrorTryExtensions
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением или ошибку исключения
        /// </summary>
        public static IResultValue<TValue> ResultValueTry<TValue>(Func<TValue> func, Func<Exception, IErrorResult> tryFunc)
        {
            TValue funcResult;

            try
            {
                funcResult = func.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultValue<TValue>(tryFunc(ex));
            }

            return new ResultValue<TValue>(funcResult);
        }
    }
}