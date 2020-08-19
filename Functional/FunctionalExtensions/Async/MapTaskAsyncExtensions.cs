using System;
using System.Threading.Tasks;

namespace Functional.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для преобразования типов для объекта-задачи
    /// </summary>
    public static class MapTaskAsyncExtensions
    {
        /// <summary>
        /// Преобразование типа-задачи с помощью функции
        /// </summary>       
        public static async Task<TResult> MapTaskAsync<TSource, TResult>(this Task<TSource> @this,
                                                                         Func<TSource, TResult> func) =>
            func(await @this);
    }
}