using System;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;
using static Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue.ResultValueTryExtensions;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений для задачи-объекта
    /// </summary>
    public static class ResultCollectionBindTryTaskAsyncExtensions
    {
        /// <summary>
        /// Связать результирующий ответ со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValueOut>> ResultValueBindTryOkTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                            Func<TValueIn, TValueOut> func, IErrorResult error) =>
            await @this.
            ResultValueBindOkTaskAsync(value => ResultValueTry(() => func.Invoke(value), error));
    }
}