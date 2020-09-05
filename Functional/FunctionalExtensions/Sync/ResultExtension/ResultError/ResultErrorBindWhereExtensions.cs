using System;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Sync.ResultExtension.ResultError
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа
    /// </summary>
    public static class ResultErrorBindWhereAsyncExtensions
    {
        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        public static IResultError ResultErrorBindOk(this IResultError @this, Func<IResultError> okFunc) =>
            @this.OkStatus
                ? okFunc.Invoke()
                : @this;
    }
}