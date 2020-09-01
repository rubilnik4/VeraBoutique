using System;
using Functional.Models.Interfaces.Result;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue.ResultValueTryExtensions;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений
    /// </summary>
    public static class ResultValueBindTryTaskAsyncExtensions
    {
        /// <summary>
        /// Связать результирующий ответ со значением с обработкой функции при положительном условии
        /// </summary>
        public static IResultValue<TValueOut> ResultValueBindTryOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                        Func<TValueIn, TValueOut> func, IErrorResult error) =>
            @this.ResultValueBindOk(value => ResultValueTry(() => func.Invoke(value), error));
    }
}