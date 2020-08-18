using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoutiqueCommon.Extensions.TaskExtensions
{
    /// <summary>
    /// Методы расширения для задач
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Выполнить все асинхронные функции и дождаться последней
        /// </summary>
        public static async Task<IList<TValue>> WaitAll<TValue>(this IEnumerable<Task<TValue>> @this) =>
            await Task.WhenAll(@this);

        /// <summary>
        /// Выполнить все асинхронные действия и дождаться последней
        /// </summary>
        public static async Task WaitAll(this IEnumerable<Task> @this) =>
            await Task.WhenAll(@this);
    }
}