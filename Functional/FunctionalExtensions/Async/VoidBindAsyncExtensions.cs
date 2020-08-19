using System;
using System.Threading.Tasks;

namespace Functional.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для асинхронных действий и объекта-задачи
    /// </summary>
    public static class VoidBindAsyncExtensions
    {
        /// <summary>
        /// Выполнить асинхронное действие, вернуть тот же тип
        /// </summary>       
        public static async Task<TValue> VoidBindAsync<TValue>(this Task<TValue> @this, Func<TValue, Task> action)
        {
            var awaitedThis = await @this;
            await action.Invoke(awaitedThis);
            return awaitedThis;
        }

        /// <summary>
        /// Выполнить асинхронное действие при положительном условии
        /// </summary>
        public static async Task<TValue> VoidOkBindAsync<TValue>(this Task<TValue> @this, Func<TValue, bool> predicate,
                                                                 Func<TValue, Task> action) =>
            await @this.
            MapBindAsync(awaitedThis => predicate(awaitedThis)
                    ? awaitedThis.VoidAsync(_ => action.Invoke(awaitedThis))
                    : Task.FromResult(awaitedThis));
    }
}