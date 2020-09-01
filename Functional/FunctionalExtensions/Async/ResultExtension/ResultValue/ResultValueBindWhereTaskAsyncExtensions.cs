using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа со значением для задачи-объекта
    /// </summary>
    public static class ResultValueBindWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Выполнение положительного условия результирующего ответа или возвращение предыдущей ошибки в результирующем ответе для задачи-объекта
        /// </summary>   
        public static async Task<IResultValue<TValueOut>> ResultValueBindOkTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                      Func<TValueIn, IResultValue<TValueOut>> okFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBindOk(okFunc));

        /// <summary>
        /// Выполнение негативного условия результирующего ответа или возвращение положительного в результирующем ответе для задачи-объекта
        /// </summary>   
        public static async Task<IResultValue<TValue>> ResultValueBindBadTaskAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IErrorResult>, IResultValue<TValue>> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBindBad(badFunc));

        /// <summary>
        /// Добавить ошибки результирующего ответа или вернуть результат с ошибками для ответа со значением для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValue>> ResultValueBindErrorsOkTaskAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                            Func<TValue, IResultError> okFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueBindErrorsOk(okFunc));
    }
}