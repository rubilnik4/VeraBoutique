using System;
using System.Threading.Tasks;

namespace Functional.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для асинхронного преобразования типов для объекта-задачи
    /// </summary>
    public static class MapBindAsyncExtensions
    {
        /// <summary>
        /// Преобразование типов с помощью асинхронной функции
        /// </summary>       
        public static async Task<TResult> MapBindAsync<TSource, TResult>(this Task<TSource> @this,
                                                                         Func<TSource, TResult> func) =>
            func(await @this);

        /// <summary>
        /// Преобразование типов с помощью асинхронной функции
        /// </summary>       
        public static async Task<TResult> MapBindAsync<TSource, TResult>(this Task<TSource> @this, 
                                                                         Func<TSource, Task<TResult>> func) =>
            await func(await @this);
    }
}