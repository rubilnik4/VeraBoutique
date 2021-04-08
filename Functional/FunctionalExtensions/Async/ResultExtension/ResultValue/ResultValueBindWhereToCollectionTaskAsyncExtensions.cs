using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Interfaces.Result;

namespace Functional.FunctionalExtensions.Async.ResultExtension.ResultValue
{
    /// <summary>
    /// Обработка условий для результирующего ответа задачи-объекта с значением с возвращением к коллекции
    /// </summary>
    public static class ResultValueBindWhereToCollectionTaskAsyncExtensions
    {
        /// <summary>
        /// Выполнение асинхронного положительного условия или возвращение предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>   
        public static async Task<IResultCollection<TValueOut>> ResultValueBindOkToCollectionTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                             Func<TValueIn, IResultCollection<TValueOut>> okFunc) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ResultValueBindOkToCollection(okFunc));
    }
}