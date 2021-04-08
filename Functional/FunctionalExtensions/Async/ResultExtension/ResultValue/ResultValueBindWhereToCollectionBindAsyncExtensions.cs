using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего ответа с значением с возвращением к коллекции
    /// </summary>
    public static class ResultValueBindWhereToCollectionBindAsyncExtensions
    {
        /// <summary>
        /// Выполнение асинхронного положительного условия или возвращение предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>   
        public static async Task<IResultCollection<TValueOut>> ResultValueBindOkToCollectionBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                             Func<TValueIn, Task<IResultCollection<TValueOut>>> okFunc) =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ResultValueBindOkToCollectionAsync(okFunc));
    }
}